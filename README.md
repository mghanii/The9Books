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

* Extract database file from "src/Api/Data/SunnahDb.rar" in the same directory, it was compressed because it exceeded github file size limit. <br/>
 <b>The original Hadith CSV files can be found in [Open-Hadith-Data](https://github.com/mhashim6/Open-Hadith-Data) repository.</b>
* Api was built using Visual Studio Community 2019 Version 16.4.0, .net core 3.1, SQLite.
* Download [SQLite & SQL Server Compact Toolbox extension](https://marketplace.visualstudio.com/items?itemName=ErikEJ.SQLServerCompactSQLiteToolbox) to access SQLite database from visual studio.

# Docker

Extract database file from "src/Api/Data/SunnahDb.rar" in the same directory.
 
Assuming your current working directory is where Dockerfile exists (src/api):
 ```
  docker build -t 9books/dev . 
  docker run   -p 5000:80 --name 9hadithbooks 9books/dev
 ```
After running these commands you should be able to access api through port 5000

# Routes
### `GET /books`
Retrieves list of all books

### `GET /{bookId}/{hadithNumber}`
Retrieves specific hadith from specific book.<br/>
<b>book id can be obtained from end point `GET /books`</b>

### `GET /{bookId}/{startHadithNumber}/{rangeSize}`
Retrieves a range of hadiths from speific book starting from specific hadith.<br/>
Maximum range size is 50.

### `GET /random`
Retrieves a random hadith from Sahih al-Bukhari.

### `GET /random/{bookId}`
Retrieves a random hadith from specific book.

