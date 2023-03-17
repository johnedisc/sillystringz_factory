


# [sillystrinzFactory]()

#### by [johnedisc](https://johnedisc.github.io)

#### an excercise in making a simple ASP.NET MVC website incorporating a database with Entity Framework

``` mermaid
flowchart TB

  DB((Factory))

  subgraph Engineers
    direction TB
    eng1("EngineerID") -->
    eng2("First") -->
    eng3("Last") -->
    eng4["List<Engineers_Machines>JoinEnt"]
  end

  subgraph Engineers_Machines
      direction TB
      em1("Engineers_MachinesId") -->
      em2("EngineerID") -->
      em3("MachineId") -->
      em4("Engineer(Model)") -->
      em5["Machine(Model)"]
  end

  subgraph Machines
      direction TB
      mac1("MachineId") -->
      mac2("Name") -->
      mac3["List<Engineers_Machines>JoinEnt"]
  end


  DB ..- Engineers
  DB ..- Machines
  DB ..- Engineers_Machines
  em5 --> eng4
  eng4 --> mac3
  eng1 --> em2
  mac1 --> em3

 %% Class Colors %%
  Note:::pink
  Engineers:::tropical
  Machines:::tropical
  Engineers_Machines:::tropical
  DB:::purple
  eng4:::purple
  mac3:::purple
  em5:::purple

  %% Colors %%

  classDef tropical fill:#C6EDC3,stroke:#000,stroke-width:2px,font-size:1.5rem,color:black
  classDef blue fill:#131761,stroke:#000,stroke-width:2px,font-size:1.5rem,color:#fff
  classDef orange fill:#ECA762,stroke:#000,stroke-width:2px,color:black,font-size:1.5rem
  classDef red fill:#FF303B,stroke:#000,stroke-width:2px,color:#fff
  classDef green fill:#027F55,stroke:#000,stroke-width:2px,color:#fff
  classDef pink fill:#E17A9B,stroke:#333,stroke-width:2px,font-size:1rem,font-weight:500,color:black
  classDef forestGreen fill:#027F55,stroke:#333,stroke-width:2px,font-size:3rem,font-weight:700
  classDef yellow fill:#FDF046,stroke:#333,stroke-width:2px,font-size:1.5rem,font-weight:700,color:black
  classDef purple fill:#D183FD,stroke:#333,stroke-width:2px,font-size:1rem,font-weight:600,color:black

  Note["*****purple entities are navigation properties 
  and exist only in session memory in the application"]
```

## technologies used

* C#
* ASP.NET 6
* entity framework
* mysql
* neovim text editor
* netcoredbg, samsung's open source dotnet debugger

## description


## setup/installation requirements

* open a terminal on your machine
* clone down the [repository from github](https://github.com/johnedisc/salon.git) inside the directory of your choosing
```bash
git clone https://github.com/johnedisc/salon.git
```
* move into the project directory (HairSalon/)
```bash
cd HairSalon.Solution/HairSalon
```
* create appsettings.json file here. first edit [yourDatabaseName], [mysqlIdName], [yourPassword] to your own settings
```bash
printf '{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[yourDatabaseName];uid=[mysqlIdName];pwd=[yourPassword];"
  }
}' > appsettings.json
```
* create a database with whatever name you choose
```bash
mysql -u YOUR_USERNAME -p
```
```sql
mysql> create database SOMENAME;
mysql> \q
```
* go back a directory and use the chris_johnedis.sql file to create a database and its tables (be sure you have mysql server >= 8 installed)
```bash
pushd ../
mysql -u username -p SOMENAME < chris_johnedis.sql
```
* go back to the project directory and run the application
```bash
pushd
dotnet run watch
```

## known Bugs

* no known bugs. layout is a work in progress.

## license

feel free to get in touch at [christopher(dot)johnedis(at)gmail(dot)com](christopher.johnedis@gmail.com)

MIT License

Copyright (c) [2023] [christopher johnedis]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

