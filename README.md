# ParallelApp - regional competition

Watch the video demo of the app [here](https://youtu.be/65cbdewrxBY) 

## Table of Contents
- [ParallelApp - regional competition](#parallelapp-earlier-version)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
    - [Ideation and Description](#ideation-and-description)
    - [Advantages of Using](#advantages-of-using)
    - [Progressive Web App](#progressive-web-app)
    - [Authors](#authors)
  - [Detailed Description](#detailed-description)
    - [Login](#login)
    - [Users](#users)
    - [Interface Description](#interface-description)
    - [Homepage / Feed](#homepage--feed)
    - [Posts](#posts)
  - [Technical Information](#technical-information)
  - [Future Plans](#future-plans)
  - [Conclusion](#conclusion)

## Introduction
ParallelApp is designed to provide schools with a simple way to publish information to all desired students, offering them a quick overview of everything important happening in the school. This version represents an earlier stage of the application.

### Ideation and Description
The idea was born out of the need to improve the traditional way of conveying information in schools. ParallelApp aims to revolutionize the way information is shared, making it more efficient and accessible.

### Advantages of Using
ParallelApp addresses the challenges of conveying small notices that pertain only to a few classes or individuals. Instead of relying on traditional methods, the app ensures that information remains accessible for a longer time.

### Progressive Web App
Being a Progressive Web App, ParallelApp can function on a wide range of browsers without needing to be published on Google Play Store or Apple Store.

### Authors
- **Teo Ivančević**: Main coder.
- **Domagoj Sabolić**: Responsible for authentication and authorization.
- **Fran Horvat**: In charge of the design and creation of wireframes and prototypes.

## Detailed Description
### Login
Users must log in to access the content. The login process includes options for password recovery.

### Users
There are two user roles:
- **Student**: Can view posts and edit some tags on their profile.
- **Admin**: Can post new posts, add, remove, and edit tags, and access information of all students in the school.

### Interface Description
The interface is simple, consisting of a content area and a navigation part.

### Homepage / Feed
The central part of the application where all school-related information and activities are found.

### Posts
Posts are the main part of the application, designed to be easily visible and sorted.

## Technical Information
- **Blazor WebAssembly and C#**: For the main development.
- **MySQL and Dapper**: For database management.
- **GitHub and Visual Studio**: For version control and development environment.
- **Azure Portal**: For hosting.
- **Auth0 Service**: For user authentication.
- **Adobe XD**: For design.
- **MudBlazor**: For UI components.

## Future Plans
- Integration with AAI@EduHr identity.
- Excel entry.
- System for managing, searching, and selecting replacements for absent teachers.
- Numerous other Quality of Life improvements.
- Real-time messaging.

## Conclusion
ParallelApp aims to bring a significant change in the way schools communicate with students. This version represents an earlier stage of the project, laying the foundation for future enhancements.

---

For more details, please refer to the [technical documentation](/Docs/Parallel%20-%20tehnička%20dokumentacija.pdf).

