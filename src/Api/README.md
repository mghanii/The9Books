# Overview
#### The nine books of Hadith:

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


## Routes
### `GET /books`
Retrieves list of all books

### `GET /{book}/{hadithNumber}`
Retrieves specific hadith from specific book.

### `GET /{book}/{startHadithNumber}/{rangeSize}`
Retrieves a range of hadiths starting from specific hadith.<br/>
If size isn't specified then default value is used (50).

### `GET /random/{book?}`
Retrieves a random hadith from specific book.<br/>
If book isn't specified then default value is used (bukhari).

