# Map Metadata

* [Resources](#resources)
* [Getting Started](#getting-started)
    * [Database Seeding](#database-seeding)
    * [Running the Project](#running-the-project)
* [JSON Metadata Generation](#json-metadata-generation)
* [Metadata Properties](#metadata-properties)
    * [Country Metadata Properties Retained](#country-metadata-properties-retained)
    * [State / Province Metadata Properties Retained](#state--province-metadata-properties-retained)
    * [Populated Places Properties Retained](#populated-places-properties-retained)
* [Database Schema](#database-schema)
* [Shapefile Metadata Models](#shapefile-metadata-models)
* [Database Seeding Strategy](#database-seeding-strategy)
* [Identified Issues](#identified-issues)
    * [Populated Places Not Corresponding to States / Provinces](#populated-places-not-corresponding-to-states--provinces)
    * [Populated Places Without ADM1NAME](#populated-places-without-adm1name)

The current intent of this project is to be able to generate a hierarchical location database based on metadata acquired from Natural Earth shapefiles. This will then enable the dynamic generation of data visualization maps based on data tied to these locations. Because the locations would be hierarchically linked, more options are available in terms of relating and visualizing the data.

## Resources
[Back to Top](#map-metadata)  

* [Natural Earth - 1:10m Cultural Vectors](https://www.naturalearthdata.com/downloads/10m-cultural-vectors/)
    * [Admin 0 - Countries](https://www.naturalearthdata.com/downloads/10m-cultural-vectors/10m-admin-0-countries/)
    * [Admin 1 - States, Provinces](https://www.naturalearthdata.com/downloads/10m-cultural-vectors/10m-admin-1-states-provinces/)
    * [Populated Places](https://www.naturalearthdata.com/downloads/10m-cultural-vectors/10m-populated-places/)
* [Yarn](https://classic.yarnpkg.com/lang/en/)
* [shapefile](https://github.com/mbostock/shapefile)
* [ndjson-cli](https://github.com/mbostock/ndjson-cli)
* [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express)
* [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
* [.NET Core SDK](https://dotnet.microsoft.com/download)
* [Node.js - Current](https://nodejs.org/en/)

## Getting Started
[Back to Top](#map-metadata)  

This project is written using a .NET Core 3.x backend and an Angular 9.x front end and uses Yarn for package management.

The project is also using Entity Framework Core linked to a development SQL Server Express instance, so you'll need to make sure the [connection string](./src/MapFramework.Web/appsettings.Development.json) corresponds correctly to your SQL Server instance.

Currently, the [Angular project](./src/MapFramework.Web/ClientApp) is a base template until I can get the location database figured out. Then, I'll start working the process of generating data visualizations using [d3-geo](https://github.com/d3/d3-geo) based on associated data.

> See the [Mapper](https://github.com/JaimeStill/Mapper) repository for the initial research leading to this project.

### Database Seeding
[Back to Top](#map-metadata)  

> You will need to make sure you have the [dotnet-ef](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet) tool correctly installed.  

1. From a command line, change into the [MapFramework.Data](./src/MapFramework.Data) directory and run: `dotnet ef migrations add "initial" -s ../MapFramework.Web`

2. Change into the [dbseeder](./src/dbseeder) directory and run: `dotnet run -- [connectionString] [mapDataDirectory]`

Note that while `\` need to be escaped in `appsettings.Development.json`, they should not be escaped on the command line.

**example**  

> Note that the `--` following `dotnet run` passes arguments to the application being run by .NET Core.

```cmd
dotnet run -- "Server=(localdb)\Projectsv13;Database=MapFramework-dev;Trusted_Connection=True;" "E:\Jaime\.me\maps\data\10m_cultural\"
```

### Running the Project
[Back to Top](#map-metadata)  

From the command line, change into the [MapFramework.Web](./src/MapFramework.Web) directory, and run `dotnet run`. After the dependencies have been installed, if they are missing, and the Angular application has compiled, you can navigate to http://localhost:5000.

## JSON Metadata Generation
[Back to Top](#map-metadata)  

The following strategy is used to extract metadata from shapefiles:

```
yarn global add shapefile ndjson-cli

shp2json -n {input shapefile} | ndjson-map "d.properties" | ndjson-reduce > {output json}

shp2json -n sources\10m_cultural\countries.shp | ndjson-map "d.properties" | ndjson-reduce > data\10m_cultural\countries.json
```

The intent is to be able to extract country, state / province, and city / town data, then be able to generate a database hierarchically linking them.

## Metadata Properties
[Back to Top](#map-metadata)  

Feature | Description
--------|------------
`featurecla` | Indicates the feature type
`scalerank` | Numbers from 0 - 9 which indicate the importance of a feature (lower is more important).

### Country Metadata Properties Retained
[Back to Top](#map-metadata)  

``` json
{
    "ABBREV": "U.S.A.",
    "ADM0_A3": "USA",
    "ADM0_A3_IS": "USA",
    "ADM0_A3_US": "USA",
    "ADMIN": "United States of America",
    "CONTINENT": "North America",
    "featurecla": "Admin-0 country",
    "FIPS_10_": "US",
    "FORMAL_EN": "United States of America",
    "GEOUNIT": "United States of America",
    "GU_A3": "USA",
    "ISO_A2": "US",
    "ISO_A3": "USA",
    "ISO_N3": "840",
    "NAME": "United States of America",
    "NAME_LONG": "United States",
    "NAME_SORT": "United States of America",
    "NE_ID": 1159321369,
    "POSTAL": "US",
    "REGION_UN": "Americas",
    "REGION_WB": "North America",
    "scalerank": 0,
    "SOV_A3": "US1",
    "SOVEREIGNT": "United States of America",
    "SUBREGION": "Northern America",
    "TYPE": "Country"
}
```

### State / Province Metadata Properties Retained
[Back to Top](#map-metadata)  

```json
{
    "abbrev": "Tex.",
    "adm0_a3": "USA",
    "adm1_code": "USA-3536",
    "admin": "United States of America",
    "code_hasc": "US.TX",
    "featurecla": "Admin-1 scale rank",
    "fips": "US48",
    "geonunit": "United States of America",
    "gn_a1_code": "US.TX",
    "gn_id": 4736286,
    "gn_name": "Texas",
    "gu_a3": "USA",
    "iso_3166_2": "US-TX",
    "iso_a2": "US",
    "latitude": 31.131,
    "longitude": -98.7607,
    "name": "Texas",
    "name_alt": "TX|Tex.",
    "name_en": "Texas",
    "ne_id": 1159315211,
    "postal": "TX",
    "region": "South",
    "region_sub": "West South Central",
    "scalerank": 2,
    "sov_a3": "US1",
    "type": "State",
    "type_en": "State"
}
```

### Populated Places Properties Retained
[Back to Top](#map-metadata)  

```json
{
    "ADM0_A3": "USA",
    "ADM0NAME": "United States of America",
    "ADM1NAME": "Texas",
    "CITYALT": "Dallas",
    "GEONAMEID": 4684888,
    "ISO_A2": "US",
    "LATITUDE": 32.8200238231,
    "LONGITUDE": -96.8400169289,
    "LS_NAME": "Dallas",
    "MEGANAME": "Dallas-Fort Worth",
    "NAME": "Dallas",
    "NAMEALT": "Dallas-Fort Worth",
    "NAMEASCII": "Dallas",
    "name_en": "Dallas",
    "ne_id": 1159151235,
    "SCALERANK": 2,
    "SOV0NAME": "United States",
    "SOV_A3": "USA",
    "TIMEZONE": "America/Chicago",
    "UN_ADM0": "United States of America",
    "UN_FID": 535,
    "UN_LAT": 32.76,
    "UN_LONG": -96.66,
}
```  

## Database Schema
[Back to Top](#map-metadata)  

The following Entity Framework Core entity classes store data extracted from their corresponding metadata files, as well as foreign key definitions establishing a locational hierarchy:

* [Country](./src/MapFramework.Data/Entities/Country.cs) - Data extracted from [countries.json](./data/10m_cultural/countries.json).
* [State](./src/MapFramework.Data/Entities/State.cs) - Data extracted from [states-provinces.json](./data/10m_cultural/states-provinces.json)
* [City](./src/MapFramework.Data/Entities/City.cs) - Data extracted from [populated-places.json](./data/10m_cultural/populated-places.json)

## Shapefile Metadata Models
[Back to Top](#map-metadata)  

The following classes are used to deserialize the shapefile metadata into a .NET Core class that can then map that data to an Entity Framework Core entity class:

* [ShapeCountry](./src/MapFramework.Data/Models/ShapeCountry.cs)
* [ShapeState](./src/MapFramework.Data/Models/ShapeState.cs)
* [ShapeCity](./src/MapFramework.Data/Models/ShapeCity.cs)

## Database Seeding Strategy
[Back to Top](#map-metadata)  

The methods contained in [DbInitializer.cs](./src/MapFramework.Data/Extensions/DbInitailizer.cs) run as the [dbseeder](./src/dbseeder) command line utility, which receives a [connection string](./src/MapFramework.Web/appsettings.Development.json) and a path string to [a directory](./data/10m_cultural/) that contains the following files:

* [countries.json](./data/10m_cultural/countries.json)
* [states-provinces.json](./data/10m_cultural/states-provinces.json)
* [populated-places.json](./data/10m_cultural/populated-places.json)

> These JSON files are generated using the strategy described in the [JSON Metadata Generation](#json-metadata-generation) section.  

The strategy for generating the data is as follows:

1. All countries are generated as is based on their metadata properties
2. States / Provinces are generated and [linked to countries](./src/MapFramework.Data/Extensions/DbInitializer.cs#L53) based on a matching `Adm0A3` value on both the country record and the state.
3. Cities are generated and [linked to states](./src/MapFramework.Data/Extensions/DbInitializer.cs#L95) based on any one of the following criteria:
    * `Adm1Name` being contained in `State.Name`
    * `Adm1Name` being contained in `State.GnName`
    * `Adm1Name` being contained in `State.NameAlt`
    * `Adm1Name` being contained in `State.NameEn`

## Identified Issues
[Back to Top](#map-metadata)  

### Populated Places Not Corresponding to States / Provinces
[Back to Top](#map-metadata)  

The following SQL Statement was run after generating the database:

```sql
select
    c.Id as 'City ID',
    c.Name as 'City',
    c.Adm1Name as 'City State',
    s.Id as 'State ID',
    s.Name as 'State',
    s.NameAlt as 'State Alt.',
    s.GnName as 'State Gn.',
    s.CountryId as 'Country ID'
from City as c
inner join State as s on c.StateId = s.Id
where s.CountryId is null
```  

The resultant output, *379 rows*, can be found in the [artifact-states.csv](./documents/artifact-states.csv) supporting document.

### Populated Places Without ADM1NAME
[Back to Top](#map-metadata)  

Laascaanood  
Ceerigaabo  
Santo AntA3nio  
Daru  
Ramallah  
Kyrenia  
San AndrAcs  
Al Khalil  
Ammochostos  
Artigas Base  
Capitol Hill  
Christiansted  
Ponce  
MayagA¼ez  
Grytviken  
Fox Bay  
George Town  
Grand Turk  
Douglas  
Neiafu  
Arrecife  
Boorama  
Burco  
Maydh  
San Marino  
Koror  
Willemstad  
Oranjestad  
Curepipe  
Vaduz  
Nablus  
Mindelo  
Capitan Arturo Prat Station  
Marambio Base  
Zucchelli Station  
Rothera Station  
Palmer Station  
Base Presidente Montalva  
Carlini Base  
King Sejong Station  
Great Wall Station  
Escudero Base  
Elephant Island  
Scott Base  
McMurdo Station  
Zhongshan Station  
Vostok  
Peter I Island  
Mirny Station  
Mawson Station  
Davis Station  
Concordia Research Station  
Casey Station  
AmundseniScott South Pole Station  
Wasa Station  
Troll Station  
Svea Station  
Novolazarevskaya Station  
Neumayer Station III  
Maitri Station  
Halley Station  
Belgrano II Base  
Sobral Base  
Aboa Station  
San MartA-n Base  
Gen. O'Higgins Base  
Esperanza Station  
Orcadas Station  
Signy Research Station  
Kullorsuaq  
Tasiusaq  
Dumont d'Urville Station  
Showa Station  
Palikir  
Majuro  
Agana  
Arecibo  
Funafuti  
Melekeok  
Bir Lehlou  
Monaco  
Tarawa  
Tasiilaq  
Moroni  
Macau  
Andorra  
Savissivik  
Kangersuatsiaq  
Uummannaq  
Santa Cruz de Tenerife  
Avarua  
Pago Pago  
Kingstown  
Castries  
Basseterre  
Las Palmas  
Berbera  
Port Louis  
Gaza  
Saint George's  
Papeete  
Manama  
Freeport  
Saint John's  
San Juan  
Stanley  
Hamilton  
Nukualofa  
Hargeysa  
Victoria  
SA£o TomAc  
Apia  
Valletta  
MalAc  
Praia  
Nassau  
Nicosia  
Singapore  
Hong Kong  

[Back to Top](#map-metadata)  