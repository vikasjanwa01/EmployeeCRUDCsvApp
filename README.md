📋 Employee CSV CRUD Application
A Windows Forms desktop application built with VB.NET that manages employee records using a CSV file as the data store. Supports full Create, Read, Update, Delete operations with audit logging.
---
📌 Table of Contents
About the Project
Features
Screenshots
Tech Stack
Project Structure
Getting Started
Prerequisites
Installation
Running the App
How It Works
CSV File Format
Form Details
FormMain
FormEdit
Validation Rules
Audit Logging
Known Issues & Fixes
Contributing
License
---
📖 About the Project
This is a simple Employee Management System that uses a `.csv` file instead of a database. It is ideal for learning Windows Forms development, CSV file handling, DataTable binding, and form validation in VB.NET.
---
✨ Features
✅ View all employee records in a DataGridView
✅ Add new employee via popup form
✅ Edit existing employee record
✅ Delete employee with confirmation dialog
✅ Data saved to `data.csv` automatically
✅ Audit log — added records saved to `added.csv`
✅ Audit log — deleted records saved to `deleted.csv`
✅ Read-only grid (no direct cell editing)
✅ Input validation on all fields
✅ Duplicate ID detection
✅ Gender stored as short code (M/F) in grid, full text in CSV
---
🖥️ Tech Stack
Technology	Details
Language	VB.NET
Framework	.NET Framework (Windows Forms)
IDE	Visual Studio
Data Storage	CSV Files
UI Components	DataGridView, TextBox, ComboBox, RadioButton
---
📁 Project Structure
```
EmployeeCsvApp/
│
├── FormMain.vb              # Main screen — displays employee list
├── FormMain.Designer.vb     # Auto-generated UI layout for FormMain
├── FormEdit.vb              # Add/Edit popup form
├── FormEdit.Designer.vb     # Auto-generated UI layout for FormEdit
├── ApplicationEvents.vb     # App-level events
│
├── data.csv                 # Main employee data file (auto-created)
├── added.csv                # Log of all added records (auto-created)
├── deleted.csv              # Log of all deleted records (auto-created)
│
└── EmployeeCsvApp.sln       # Visual Studio solution file
```
---
🚀 Getting Started
Prerequisites
Windows OS
Visual Studio 2019 / 2022 (Community edition is free)
.NET Framework 4.7.2 or higher
---
Installation
Clone the repository
```bash
   git clone https://github.com/your-username/EmployeeCsvApp.git
   ```
Open the solution
Launch Visual Studio
Click `File` → `Open` → `Project/Solution`
Select `EmployeeCsvApp.sln`
Build the project
Press `Ctrl + Shift + B` or click `Build` → `Build Solution`
---
Running the App
Press `F5` to run in Debug mode
Or press `Ctrl + F5` to run without debugging
> On first run, place a `data.csv` file in the same folder as the `.exe`  
> (usually `bin\Debug\`) or the app will show a "CSV file not found" message.
---
⚙️ How It Works
```
App Starts
    ↓
FormMain loads → reads data.csv → fills DataTable → shows in DataGridView
    ↓
User clicks Add  → opens FormEdit (blank)  → fills form → saves to data.csv + added.csv
User clicks Edit → opens FormEdit (filled) → edits      → saves to data.csv
User clicks Delete → confirms → removes from data.csv → logs to deleted.csv
```
---
📄 CSV File Format
All three CSV files share the same column structure:
```
ID,Name,Age,Gender,Department,EmploymentType
1,John Smith,30,Male,IT,Full-Time
2,Jane Doe,25,Female,HR,Part-Time
```
Column	Type	Example
ID	Integer	101
Name	String (letters only)	John Smith
Age	Integer (1–120)	28
Gender	Male / Female	Male
Department	Dropdown value	IT
EmploymentType	Full-Time / Part-Time	Full-Time
> **Note:** Gender is stored as `Male`/`Female` in the CSV but displayed as `M`/`F` in the grid.
---
🖼️ Form Details
FormMain
The main window that shows all employee data.
Control	Purpose
`dgvData`	DataGridView — displays employee records (read-only)
`btnAdd`	Opens FormEdit to add a new employee
`btnEdit`	Opens FormEdit pre-filled with selected row
`btnDelete`	Deletes selected employee after confirmation
DataGridView is set to read-only:
```vb
dgvData.ReadOnly = True
dgvData.AllowUserToAddRows = False
dgvData.AllowUserToDeleteRows = False
```
---
FormEdit
A popup dialog used for both adding and editing employees.
Control	Field
`txtID`	Employee ID (numbers only)
`txtName`	Employee Name (letters and spaces only)
`txtAge`	Age (numbers only, 1–120)
`rbMale` / `rbFemale`	Gender radio buttons
`cmbDepartment`	Department dropdown
`rbFullTime` / `rbPartTime`	Employment type radio buttons
`btnSave`	Validates and saves data
`btnCancel`	Closes form without saving
---
✔️ Validation Rules
Field	Rule
ID	Required, must be numeric, must be unique
Name	Required, letters and spaces only
Age	Required, numeric, between 1 and 120
Gender	Must select Male or Female
Department	Must select from dropdown
Employment Type	Must select Full-Time or Part-Time
Invalid fields are highlighted in Light Pink with an error label shown near the ID field.
---
📝 Audit Logging
The app automatically logs changes to separate CSV files:
File	When it is updated
`added.csv`	Every time a new employee is added
`deleted.csv`	Every time an employee is deleted
The header row is written only once (on first creation of the file).
---
🐛 Known Issues & Fixes
NullReferenceException on LoadCSV
Cause: Windows CSV files use `\r\n` line endings. The `\r` character was not stripped before splitting, causing `parts(5)` to be malformed.
Fix: Trim each line before splitting:
```vb
Dim line As String = lines(i).Trim()
Dim parts = line.Split(","c)
```
---
NullReferenceException on Add/Delete
Cause: `LoadCSV()` was calling `dt.Columns.Clear()` and `dgvData.DataSource = dt` on every reload, breaking the DataGridView binding.
Fix:
Define columns once in `FormMain_Load`
Bind `DataSource` once in `FormMain_Load`
`LoadCSV()` only calls `dt.Rows.Clear()` — never touches columns or DataSource
---
🤝 Contributing
Contributions are welcome!
Fork the repository
Create a new branch
```bash
   git checkout -b feature/your-feature-name
   ```
Make your changes and commit
```bash
   git commit -m "Add: your feature description"
   ```
Push to your branch
```bash
   git push origin feature/your-feature-name
   ```
Open a Pull Request
---
📃 License
This project is licensed under the MIT License.
---
👤 Author
Vikash Janwa  
GitHub: @vikasjanwa01
---
> ⭐ If you found this project helpful, please give it a star!
