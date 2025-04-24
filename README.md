# MELC-OPEN

**MELC-OPEN** is a web application designed for the management, control, and billing of manufacturing processes involving parts and machines. It aims to streamline operations, enhance productivity, and provide real-time insights into manufacturing workflows.

## 🚀 Features

- **Comprehensive Management**: Oversee the entire manufacturing process, from order initiation to completion.
- **Control Mechanisms**: Monitor machine performance, track part production, and ensure quality standards.
- **Billing Integration**: Automate invoicing based on production data, reducing manual errors and delays.
- **User Authentication**: Secure login system to protect sensitive manufacturing data.
- **PDF Generation**: Produce detailed reports and invoices in PDF format for record-keeping and client communication.

## 🛠️ Technologies Used

- **Frontend**: HTML, CSS, JavaScript
- **Backend**: C# (.NET Framework)
- **API Development**: ASP.NET Core Web API
- **PDF Generation**: Integrated PDF facade for report creation
- **Containerization**: Docker for environment consistency and deployment
- **Project Structure**:
  - `MELC.Core`: Core business logic and models
  - `MELC.Identidade.API`: Authentication and identity management
  - `MELC.PDF.Facade`: PDF generation utilities
  - `MELC.Tickets.API`: Ticketing and issue tracking
  - `MELC.WebApi.Core`: Core API functionalities
  - `MELC.WebApp.MVC`: MVC-based web application interface

## 📄 Usage

Once the application is running:

- Access the web interface via `http://localhost:5000` (or the port specified in your Docker configuration).
- Log in using your credentials.
- Navigate through the dashboard to manage manufacturing orders, monitor machine statuses, and generate billing reports.

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/YourFeatureName
   ```
3. Commit your changes:
   ```bash
   git commit -m 'Add your feature'
   ```
4. Push to the branch:
   ```bash
   git push origin feature/YourFeatureName
   ```
5. Open a pull request.

Please ensure your code adheres to the project's coding standards and includes appropriate tests.

For questions or support, please contact [brenodasilvaa](https://github.com/brenodasilvaa).

