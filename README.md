# BAU

Brief Description

  My implementation allows a user to populate the next 2 free weeks in a Schedule as per the requirements. 

  They can also create and update Engineer information, including making an Engineer unavailable for scheduling.

Where You Can See It

  Demo http://amigo-bau.azurewebsites.net
  Code https://github.com/chrisfildes/bau

Overall Architecture
  
  ASP.NET Core 2.0
  Hosted on Azure
  SQL Server Azure database

API

  Data Layer (using Entity Framework Code First)

Business Layer

  Loosely coupled so either can be swapped out

Presentation Layer
  
  ASP.NET MVC Core using Razor
  Bootstrap 
  Uses minimised versions of css/js on production site
  Using Dependency Injection to select which Business Layer / Data Layer to use

Pages
  
  Homepage (just holding page)
  Schedule (to view upcoming schedule and populate the next slot)
  Engineers (to view all Engineers with ability to add and amend details
  
Structure Overview

  Data Layer

    BAU.Data
      Interfaces
        IEngineerRepository
        IRepository
        ISupportSlotRepository
      Models
        Engineer
        Schedule
        SupportSlot
    
    BAU.Data.EntityFramework
      Repositories
        EngineerRepository
        SupportSlotRepository 
  
  Business Layer
    
    BAU.Business
      Interfaces
        IEngineerPool
        IEngineerService
        IScheduleService
    
    BAU.Business.Implementation 
      Services
        EngineerPool
        EngineerService
        ScheduleService
    
   Presentation Layer
    
    BAU.Web
      Controllers
        EngineersController
        HomeController
        ScheduleController
      Models
        EngineerCreateModel
        EngineerEditModel
        ErrorViewModel
      Views
        Engineers
          Create
          Edit
          Index
        Home
          Index 
        Schedule
          Index
        Shared
          Error
          ScheduleDates
          _Layout
          _ValidationScriptsPartial

What I would like to do next

  Test projects for Data Layer, Business Layer and Presentation Layer.
  Web API 
  Client-side framework integration 
  Authentication 
