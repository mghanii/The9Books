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


# Development Setup

* Api was built using Visual Studio Community 2019 Version 16.4.0, .net core 3.1, SQLite.
* Download [SQLite & SQL Server Compact Toolbox extension](https://marketplace.visualstudio.com/items?itemName=ErikEJ.SQLServerCompactSQLiteToolbox) to access SQLite database from visual studio.

## Starting the Development Server

Open up Terminal and navigate to the directory where you want the project to live.

Clone this repository:

```
git clone https://github.com/MohamedAbdelghani/The9Books.git
```

Extract database file from "src/Api/Data/SunnahDb.rar" in the same directory, it was compressed because it exceeded github file size limit. <br/>
<b>The original Hadith CSV files can be found in [Open-Hadith-Data](https://github.com/mhashim6/Open-Hadith-Data) repository.</b> 
 
Navigate to  api project:

```
cd The9Books/src/Api
```

Ensure that Docker Desktop is up and running, then run the following commands:
```
  docker build -t 9books/dev . 
  docker run   -p 5000:80 --name 9hadithbooks 9books/dev
 ```
Wait for the logs to show "server started on port 5000", then navigate to localhost:5000 to access api.

# Routes
### `GET /books`
Retrieves list of all books

### `GET /{bookId}/{hadithNumber}`
Retrieves specific hadith from specific book.<br/>
<b>book ids can be obtained from end point `GET /books`</b>

### `GET /{bookId}/{startHadithNumber}/{rangeSize}`
Retrieves a range of hadiths from speific book starting from specific hadith.<br/>
Maximum range size is 50.

### `GET /random`
Retrieves a random hadith from Sahih al-Bukhari.

### `GET /random/{bookId}`
Retrieves a random hadith from specific book.

