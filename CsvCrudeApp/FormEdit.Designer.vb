<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEdit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        txtID = New TextBox()
        txtName = New TextBox()
        txtAge = New TextBox()
        cmbDepartment = New ComboBox()
        rbMale = New RadioButton()
        rbFemale = New RadioButton()
        rbFullTime = New RadioButton()
        rbPartTime = New RadioButton()
        btnSave = New Button()
        btnCancel = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        grpGender = New GroupBox()
        Label4 = New Label()
        grpEmployment = New GroupBox()
        Label7 = New Label()
        lblIdError = New Label()
        grpGender.SuspendLayout()
        grpEmployment.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtID
        ' 
        txtID.Location = New Point(352, 206)
        txtID.Name = "txtID"
        txtID.Size = New Size(322, 31)
        txtID.TabIndex = 0
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(352, 276)
        txtName.Name = "txtName"
        txtName.Size = New Size(322, 31)
        txtName.TabIndex = 1
        ' 
        ' txtAge
        ' 
        txtAge.Location = New Point(352, 344)
        txtAge.Name = "txtAge"
        txtAge.Size = New Size(322, 31)
        txtAge.TabIndex = 2
        ' 
        ' cmbDepartment
        ' 
        cmbDepartment.FormattingEnabled = True
        cmbDepartment.Location = New Point(352, 473)
        cmbDepartment.Name = "cmbDepartment"
        cmbDepartment.Size = New Size(322, 33)
        cmbDepartment.TabIndex = 3
        ' 
        ' rbMale
        ' 
        rbMale.AutoSize = True
        rbMale.Location = New Point(210, 13)
        rbMale.Name = "rbMale"
        rbMale.Size = New Size(92, 36)
        rbMale.TabIndex = 4
        rbMale.TabStop = True
        rbMale.Text = "Male"
        rbMale.UseVisualStyleBackColor = True
        ' 
        ' rbFemale
        ' 
        rbFemale.AutoSize = True
        rbFemale.Location = New Point(395, 14)
        rbFemale.Name = "rbFemale"
        rbFemale.Size = New Size(116, 36)
        rbFemale.TabIndex = 5
        rbFemale.TabStop = True
        rbFemale.Text = "Female"
        rbFemale.UseVisualStyleBackColor = True
        ' 
        ' rbFullTime
        ' 
        rbFullTime.AutoSize = True
        rbFullTime.Font = New Font("Segoe UI", 12.0F)
        rbFullTime.Location = New Point(217, 25)
        rbFullTime.Name = "rbFullTime"
        rbFullTime.Size = New Size(137, 36)
        rbFullTime.TabIndex = 6
        rbFullTime.TabStop = True
        rbFullTime.Text = "Full Time"
        rbFullTime.UseVisualStyleBackColor = True
        ' 
        ' rbPartTime
        ' 
        rbPartTime.AutoSize = True
        rbPartTime.Font = New Font("Segoe UI", 12.0F)
        rbPartTime.Location = New Point(387, 25)
        rbPartTime.Name = "rbPartTime"
        rbPartTime.Size = New Size(139, 36)
        rbPartTime.TabIndex = 7
        rbPartTime.TabStop = True
        rbPartTime.Text = "Part Time"
        rbPartTime.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(294, 658)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(179, 67)
        btnSave.TabIndex = 8
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(599, 658)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(179, 67)
        btnCancel.TabIndex = 9
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 12.0F)
        Label1.Location = New Point(185, 212)
        Label1.Name = "Label1"
        Label1.Size = New Size(37, 32)
        Label1.TabIndex = 10
        Label1.Text = "ID"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12.0F)
        Label2.Location = New Point(185, 341)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 32)
        Label2.TabIndex = 11
        Label2.Text = "Age"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12.0F)
        Label3.Location = New Point(185, 282)
        Label3.Name = "Label3"
        Label3.Size = New Size(123, 32)
        Label3.TabIndex = 12
        Label3.Text = "Full Name"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12.0F)
        Label5.Location = New Point(185, 476)
        Label5.Name = "Label5"
        Label5.Size = New Size(142, 32)
        Label5.TabIndex = 14
        Label5.Text = "Department"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 12.0F)
        Label6.Location = New Point(44, 25)
        Label6.Name = "Label6"
        Label6.Size = New Size(112, 32)
        Label6.TabIndex = 15
        Label6.Text = "Job-Type"
        ' 
        ' grpGender
        ' 
        grpGender.Controls.Add(Label4)
        grpGender.Controls.Add(rbFemale)
        grpGender.Controls.Add(rbMale)
        grpGender.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        grpGender.Location = New Point(185, 405)
        grpGender.Margin = New Padding(0)
        grpGender.Name = "grpGender"
        grpGender.Size = New Size(551, 53)
        grpGender.TabIndex = 16
        grpGender.TabStop = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(0, 12)
        Label4.Name = "Label4"
        Label4.Size = New Size(92, 32)
        Label4.TabIndex = 18
        Label4.Text = "Gender"
        Label4.TextAlign = ContentAlignment.TopCenter
        ' 
        ' grpEmployment
        ' 
        grpEmployment.Controls.Add(Label6)
        grpEmployment.Controls.Add(rbPartTime)
        grpEmployment.Controls.Add(rbFullTime)
        grpEmployment.Location = New Point(141, 525)
        grpEmployment.Name = "grpEmployment"
        grpEmployment.Size = New Size(540, 78)
        grpEmployment.TabIndex = 17
        grpEmployment.TabStop = False
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(352, 240)
        Label7.Name = "Label7"
        Label7.Size = New Size(0, 25)
        Label7.TabIndex = 18
        ' 
        ' lblIdError
        ' 
        lblIdError.AutoSize = True
        lblIdError.ForeColor = Color.Red
        lblIdError.Location = New Point(361, 247)
        lblIdError.Name = "lblIdError"
        lblIdError.Size = New Size(0, 25)
        lblIdError.TabIndex = 19
        ' 
        ' FormEdit
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1262, 788)
        Controls.Add(lblIdError)
        Controls.Add(Label7)
        Controls.Add(grpEmployment)
        Controls.Add(grpGender)
        Controls.Add(Label5)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(btnCancel)
        Controls.Add(btnSave)
        Controls.Add(cmbDepartment)
        Controls.Add(txtAge)
        Controls.Add(txtName)
        Controls.Add(txtID)
        Name = "FormEdit"
        grpGender.ResumeLayout(False)
        grpGender.PerformLayout()
        grpEmployment.ResumeLayout(False)
        grpEmployment.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtID As TextBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents txtAge As TextBox
    Friend WithEvents cmbDepartment As ComboBox
    Friend WithEvents rbMale As RadioButton
    Friend WithEvents rbFemale As RadioButton
    Friend WithEvents rbFullTime As RadioButton
    Friend WithEvents rbPartTime As RadioButton
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents grpGender As GroupBox
    Friend WithEvents grpEmployment As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblIdError As Label
End Class
