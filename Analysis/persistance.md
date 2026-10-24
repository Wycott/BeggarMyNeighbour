# Persistance

Containers are often stopped so we lose track of the best runs in terms of cards, decks and tricks.

So we want to be able to store the best runs in a postgres database.

To do this, we'll add 2 new services and a database.

One service will only be able to write to the database - writer service

One service will only be able to read the database - reader service

New menu options will be added to the web application each with its own page

The Archive page will write to the database via the writer service if any one of these conditions is met:

- More cards than current maximum
- More tricks than the current maximum
- More decks than the current maximum

The Records page will read from the database via the reader service.

On the Records page will be the top N entries where N is stored in the config.

On the Records page there will be a table containing the top N entries with these columns:

- GUID
- Recorded (UTC)
- Cards
- Decks
- Tricks
- Release

If any row is clicked, then more detail will be displayed below the table in a similar way to the Simulators page:

Iterations: 909,564,349	
Runtime: 5h 55m

P1 (9 court cards, Lose): -----A--Q---KAJQQ-------JJ
P2 (7 court cards, Win): ------K-KA--------K-A--QJ-

