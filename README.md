# ASP.NET WebApi with Mediator Pattern and Docker Support

This ASP.NET WebApi project is designed using the Mediator pattern to handle commands and queries. It utilizes .NET 8.0 and provides an organized structure for handling HTTP requests and performing operations in the application layer. Additionally, it's Dockerized for easy deployment and scalability.

## Description

The Mediator pattern facilitates communication between components of a system without them directly knowing each other. In this project, the Mediator pattern is implemented to handle commands and queries within the application, enabling lower coupling between different components and better separation of concerns.

## Project Structure

- **Controllers**: Contains controllers that handle incoming HTTP requests and route them to the corresponding handlers.
- **Mediator**: Contains classes related to implementing the Mediator pattern, such as the Mediator itself, commands, queries, and command/query handlers.
- **Interfaces**: Contains interfaces used for application services that encapsulate business logic. These interfaces are implemented by command/query handlers to perform the desired operations.
- **Dockerfile**: Defines the Docker image configuration for building and running the application in a containerized environment.

## System Requirements

- .NET 8.0 or higher
- Visual Studio 2022 or a compatible code editor
- Docker (if running the application in a containerized environment)

## Installation

1. Clone this repository to your local machine:
   
   `git clone https://github.com/xexubonete/webapi-docker.git`

3. Open the project in Visual Studio or your preferred code editor.

4. Build the solution and run the application.

## Usage

- Access the endpoints provided by the controllers to send HTTP requests and execute commands or queries.
- Command and query handlers will process the requests and return the corresponding responses.

## Docker Support

This project is Dockerized, allowing easy deployment and scalability. To run the application using Docker, follow these steps:

1. Make sure you have Docker installed on your machine.
2. Navigate to the root directory of the project in your terminal.
3. Build the Docker image using the provided Dockerfile:

   `docker build -t webapi-app .`
   
5. Once the image is built, run a Docker container:

   `docker run -d -p 8080:80 --name webapi-container webapi-app`

6. Access the application through `http://localhost:8080` in your web browser or any HTTP client.

## Contribution

If you wish to contribute to this project, follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/feature-name`).
3. Make your changes and commit (`git commit -am 'Add new feature'`).
4. Push the branch (`git push origin feature/feature-name`).
5. Create a new pull request.

## Authors

- Jesus Bonete (xexubonete)

## License

This project is licensed under the [MIT License](LICENSE).






