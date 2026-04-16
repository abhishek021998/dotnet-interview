Problems Identified: What issues did you find in the original implementation?

-> In Controller class we can see  "var todoService = new TodoService();" This is not a good practice as it creates a tight coupling between the Controller and the Service. It makes it difficult to test the Controller in isolation and also makes it harder to change the implementation of the Service without affecting the Controller.
-> The TodoService class having database connection string hardoded but it should pick from configuration file. This is not a good practice as it makes it difficult to change the connection string without modifying the code. It also makes it harder to manage different environments (development, staging, production) where the connection string may differ.
-> The TodoService class is not implementing any interface. This makes it difficult to mock the service for unit testing and also makes it harder to change the implementation of the service without affecting the consumers of the service. It would be better to define an interface for the TodoService and have the implementation class implement that interface. This would allow for better separation of concerns and make it easier to test and maintain the code.
-> The TodoService class is not handling exceptions properly. If there is an error while connecting to the database or executing a query, it will throw an exception that is not caught. This can lead to unhandled exceptions and crashes in the application. It would be better to implement proper error handling and logging in the service to ensure that any issues are properly handled and logged for troubleshooting.
-> Added Global exception handling middleware to handle any unhandled exceptions in the application and return a proper error response to the client. This will help in improving the overall stability and reliability of the application.
-> Unit test cases are added for the Controller and Service classes to ensure that the application is working as expected and to catch any potential issues early in the development process. This will help in improving the overall quality of the code and make it easier to maintain and extend the application in the future.

Architectural Decisions: What architectural patterns or principles did you apply in your solution?

-> We have used Clean Architecture principles to separate the concerns of the application. The Controller is responsible for handling HTTP requests and responses, while the Service is responsible for business logic and data access. This separation allows for better maintainability and testability of the code.
-> I applied the Dependency Injection principle to decouple the Controller from the Service. Instead of creating an instance of the TodoService directly in the Controller, I used constructor injection to inject an instance of the ITodoService interface. This allows for better separation of concerns and makes it easier to test the Controller in isolation.


How to Run: Clear instructions for running the application and tests

-> To run the application, follow these steps:
1. Clone the repository to your local machine.
2. Navigate to the project directory in the terminal.
3. Run the command "dotnet run" to start the application. The API will be available at "https://localhost:5001" or "http://localhost:5000".
4. You can use tools like Postman or curl to test the API endpoints. For example, to get all todos, you can send a GET request to "https://localhost:5001/api/todos". To create a new todo, you can send a POST request to "https://localhost:5001/api/todos" with the todo data in the request body.


API Documentation: How to use the endpoints

-> The API has the following endpoints:
1. GET /api/todos/{id}: Retrieves a specific todo by its ID.
2. POST /api/todos: Creates a new todo. The request body should contain the todo data in JSON format, for example: {"title": "Buy groceries", "isCompleted": false}.
3. PUT /api/todos/{id}: Updates an existing todo. The request body should contain the updated todo data in JSON format, for example: {"title": "Buy groceries", "isCompleted": true}.
4. DELETE /api/todos/{id}: Deletes a specific todo by its ID.


Future Improvements: What would you do if you had more time?

-> If I had more time, I would implement authentication and authorization for the API to ensure that only authorized users can access and modify the todos. This could be done using JWT (JSON Web Tokens) or another authentication mechanism. I would also implement pagination for the GET /api/todos endpoint to improve performance when there are a large number of todos. Additionally, I would add more comprehensive unit tests and integration tests to ensure the reliability and stability of the application. Finally, I would consider implementing a caching mechanism to improve performance for frequently accessed data.
-> We could use LLM to generate API documentation automatically based on the code and comments in the application. This would help in keeping the documentation up-to-date and reduce the effort required to maintain it manually. We could also use LLM to generate test cases based on the API endpoints and expected behavior, which would help in improving test coverage and ensuring that the application is thoroughly tested.
