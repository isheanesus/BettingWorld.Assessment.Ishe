This project uses Docker and Docker compose to run the API, Redis and MySQL

1. Ensure the Docker Desktop is running
2. Run the project from docker-compose context (this should pull and setup containers and run the migration to setup the database.) If the migration fails the project should still run
3. Should the browser not open automatically use this url to access swagger https://localhost:5001/swagger/index.html
4. There are 2 endpoints Convert/getConversion (converts an amount from one currency to any other) and Rates/history (gets the rates history from the database)
