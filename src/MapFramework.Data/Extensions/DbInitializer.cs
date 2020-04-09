using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MapFramework.Data.Entities;
using MapFramework.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MapFramework.Data.Extensions
{
    public static class DbInitializer
    {
        public static async Task Initialize(this AppDbContext db, string mapData)
        {
            Console.WriteLine("Initializing database");
            await db.Database.MigrateAsync();
            Console.WriteLine("Database initialized");
            Console.WriteLine();

            Console.WriteLine("Creating countries");
            await db.GenerateCountries(mapData);

            Console.WriteLine("Creating states");
            await db.GenerateStates(mapData);

            Console.WriteLine("Creating cities");
            await db.GenerateCities(mapData);
        }

        static async Task GenerateCountries(this AppDbContext db, string mapData)
        {
            using var countryJson = File.OpenRead($"{mapData}countries.json");
            var countries = await JsonSerializer.DeserializeAsync<ShapeCountry[]>(countryJson);
            await db.Countries.AddRangeAsync(ShapeCountry.ToCountryList(countries));
            await db.SaveChangesAsync();
        }

        static async Task GenerateStates(this AppDbContext db, string mapData)
        {
            using var stateJson = File.OpenRead($"{mapData}states-provinces.json");
            var shapeStates = await JsonSerializer.DeserializeAsync<ShapeState[]>(stateJson);
            var states = ShapeState.ToStateList(shapeStates);
            await states.MatchCountries(db);
        }

        static async Task MatchCountries(this IEnumerable<State> states, AppDbContext db)
        {
            foreach (var state in states)
                await state.MatchCountry(db);
        }

        static async Task MatchCountry(this State state, AppDbContext db)
        {
            var country = await db.Countries.FirstOrDefaultAsync(x => x.Adm0A3.ToLower() == state.Adm0A3.ToLower());

            if (country != null)
            {
                state.CountryId = country.Id;
            }
            else
            {
                country = new Country
                {
                    Adm0A3 = state.Adm0A3,
                    GuA3 = state.GuA3,
                    IsoA2 = state.IsoA2,
                    Admin = state.Admin
                };

                await db.Countries.AddAsync(country);
                await db.SaveChangesAsync();

                state.CountryId = country.Id;
            }

            await db.States.AddAsync(state);
            await db.SaveChangesAsync();
        }

        static async Task GenerateCities(this AppDbContext db, string mapData)
        {
            using var cityJson = File.OpenRead($"{mapData}populated-places.json");
            var shapeCities = await JsonSerializer.DeserializeAsync<ShapeCity[]>(cityJson);
            var cities = ShapeCity.ToCityList(shapeCities);
            await cities.MatchStates(db);
        }

        static async Task MatchStates(this IEnumerable<City> cities, AppDbContext db)
        {
            foreach (var city in cities)
                await city.MatchState(db);
        }

        static async Task MatchState(this City city, AppDbContext db)
        {
            if (!string.IsNullOrEmpty(city.Adm1Name))
            {
                var state = await db.States.FirstOrDefaultAsync(x =>
                    (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(city.Adm1Name.ToLower())) ||
                    (!string.IsNullOrEmpty(x.GnName) && x.GnName.ToLower().Contains(city.Adm1Name.ToLower())) ||
                    (!string.IsNullOrEmpty(x.NameAlt) && x.NameAlt.ToLower().Contains(city.Adm1Name.ToLower())) ||
                    (!string.IsNullOrEmpty(x.NameEn) && x.NameEn.ToLower().Contains(city.Adm1Name.ToLower()))
                );

                if (state != null)
                {
                    city.StateId = state.Id;
                }
                else
                {
                    state = new State
                    {
                        Name = city.Adm1Name,
                        GnName = city.Adm1Name
                    };

                    await db.States.AddAsync(state);
                    await db.SaveChangesAsync();

                    city.StateId = state.Id;
                }

                await db.Cities.AddAsync(city);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine($"{city.Name} does not have a state");
            }
        }
    }
}