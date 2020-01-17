# Overview
#### An API to retrieve hadith of nine famous books:

|  Book 	|  Hadith count 	|
|---	|---	|
|   	 Sahih Bukhari|  7008 	|
|   Sahih Muslim	| 5362  	|
|   Sunan Nasai	|   5662	|
|   Sunan Abi Dawud	|   4590	|
|   	Sunan Tirmidhi|   3891	|
|   	Sunan Ibn Majah|  4332 	|
|   	Muwatta Imam Malik|  1594 	|
|    Sunan Darimi	|   3367	|
|   	Musnad Ahmad|   26363	|


# Development

* Extract database file from "src/Api/Data/SunnahDb.rar" in the same directory, it was compressed because it exceeded github file size limit. 
* Api was built using Visual Studio Community 2019 Version 16.4.0, .net core 3.1, SQLite.
* Download [SQLite & SQL Server Compact Toolbox extension](https://marketplace.visualstudio.com/items?itemName=ErikEJ.SQLServerCompactSQLiteToolbox) to access SQLite database from visual studio.

## Routes
### `GET /books`
Retrieves list of all books

### `GET /{book}/{hadithNumber}`
Retrieves specific hadith from specific book.

### `GET /{book}/{startHadithNumber}/{rangeSize}`
Retrieves a range of hadiths starting from specific hadith.<br/>
Maximum range size is 50.

### `GET /random/{book?}`
Retrieves a random hadith from specific book.<br/>
If book isn't specified then default value is used (bukhari).

