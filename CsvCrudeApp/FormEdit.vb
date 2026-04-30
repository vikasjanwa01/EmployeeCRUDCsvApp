Public Class FormEdit

    Public resultData As String()
    Private originalID As String = ""
    ' ================= LOAD =================
    Public Sub New()
        InitializeComponent()

        cmbDepartment.Items.AddRange({
        "IT", "HR", "Finance", "Marketing",
        "Operations", "Sales", "Legal", "R&D"
    })
    End Sub


    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        ' Allow only letters, space and backspace
        If Not Char.IsLetter(e.KeyChar) AndAlso
       e.KeyChar <> " "c AndAlso
       e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtID_TextChanged(sender As Object, e As EventArgs) Handles txtID.TextChanged

        Dim idText As String = txtID.Text.Trim()

        ' Reset
        lblIdError.Text = ""
        txtID.BackColor = Color.White
        btnSave.Enabled = True

        ' Empty
        If idText = "" Then
            lblIdError.Text = "ID is required"
            txtID.BackColor = Color.LightPink
            btnSave.Enabled = False
            Exit Sub
        End If

        ' Not numeric
        If Not Integer.TryParse(idText, Nothing) Then
            lblIdError.Text = "ID must be a number"
            txtID.BackColor = Color.LightPink
            btnSave.Enabled = False
            Exit Sub
        End If

        ' Duplicate
        If IsDuplicateID(idText) Then
            lblIdError.Text = "ID already exists"
            txtID.BackColor = Color.LightPink
            btnSave.Enabled = False
            Exit Sub
        End If

    End Sub


    Private Sub txtID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtID.KeyPress
        ' Allow only numbers and backspace
        If Not Char.IsDigit(e.KeyChar) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub


    Private Function IsDuplicateID(id As String) As Boolean

        Dim filePath As String = Application.StartupPath & "\data.csv"

        If Not IO.File.Exists(filePath) Then Return False

        Dim lines = IO.File.ReadAllLines(filePath)

        For i As Integer = 1 To lines.Length - 1

            If String.IsNullOrWhiteSpace(lines(i)) Then Continue For

            Dim parts = lines(i).Split(","c)

            ' ⭐ Ignore same record in edit mode
            If parts(0) = id AndAlso id <> originalID Then
                Return True
            End If

        Next

        Return False
    End Function


    ' ================= SET DATA =================
    Public Sub SetData(id As String, name As String, age As String,
                       gender As String, dept As String, empType As String)

        originalID = id

        txtID.Text = id
        txtName.Text = name
        txtAge.Text = age

        ' Department (safe selection)
        If cmbDepartment.Items.Contains(dept) Then
            cmbDepartment.SelectedItem = dept
        Else
            cmbDepartment.Text = dept ' fallback
        End If


        ' Gender
        If gender = "Male" Then
            rbMale.Checked = True
        ElseIf gender = "Female" Then
            rbFemale.Checked = True
        End If

        ' Employment Type
        If empType = "Full-Time" Then
            rbFullTime.Checked = True
        ElseIf empType = "Part-Time" Then
            rbPartTime.Checked = True
        End If
    End Sub

    ' ================= SAVE =================
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If txtID.Text.Trim() = "" Or Not IsNumeric(txtID.Text) Then
            MessageBox.Show("ID must be a number")
            Exit Sub
        End If

        ' ===== ID VALIDATION =====
        If txtID.Text.Trim() = "" Or Not Integer.TryParse(txtID.Text, Nothing) Then
            lblIdError.Text = "ID must be a number"
            txtID.BackColor = Color.Red
            Exit Sub
        End If

        ' ===== DUPLICATE CHECK =====
        Dim idText As String = txtID.Text.Trim()

        If IsDuplicateID(idText) AndAlso idText <> originalID Then
            lblIdError.Text = "ID already exists"
            txtID.BackColor = Color.Red
            Exit Sub
        End If
        ' ===== VALIDATION =====
        If txtID.Text = "" Then
            MessageBox.Show("ID is required") : Exit Sub
        End If

        'If txtName.Text = "" Then
        '    MessageBox.Show("Name is required") : Exit Sub
        'End If


        If txtName.Text.Trim() = "" OrElse Not txtName.Text.All(Function(c) Char.IsLetter(c) Or c = " "c) Then
            MessageBox.Show("Name should contain only alphabets")
            txtName.BackColor = Color.LightPink
            Exit Sub
        Else
            txtName.BackColor = Color.White
        End If




        'If txtAge.Text = "" Or Not IsNumeric(txtAge.Text) Then
        '    MessageBox.Show("Enter valid Age") : Exit Sub
        'End If



        Dim ageVal As Integer

        If Not Integer.TryParse(txtAge.Text.Trim(), ageVal) Then
            MessageBox.Show("Age must be a number")
            txtAge.BackColor = Color.LightPink
            Exit Sub
        End If

        ' ✅ Realistic age check
        If ageVal < 1 Or ageVal > 120 Then
            MessageBox.Show("Enter a realistic age ")
            txtAge.BackColor = Color.LightPink
            Exit Sub
        Else
            txtAge.BackColor = Color.White
        End If




        If cmbDepartment.SelectedItem Is Nothing Then
            MessageBox.Show("Select Department") : Exit Sub
        End If

        If Not rbMale.Checked And Not rbFemale.Checked Then
            MessageBox.Show("Select Gender") : Exit Sub
        End If

        If Not rbFullTime.Checked And Not rbPartTime.Checked Then
            MessageBox.Show("Select Employment Type") : Exit Sub
        End If

        ' ===== VALUES =====
        Dim gender As String = If(rbMale.Checked, "Male", "Female")
        Dim empType As String = If(rbFullTime.Checked, "Full-Time", "Part-Time")

        ' ✅ Confirmation before save
        Dim frm As New FormConfirm("Do you want to save this data?")
        frm.ShowDialog(Me)
        If frm.UserChoice = False Then Exit Sub

        resultData = {
            txtID.Text,
            txtName.Text,
            txtAge.Text,
            gender,
            cmbDepartment.SelectedItem.ToString(),
            empType
        }

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        ' Your logic here (or leave empty)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub


    Private Sub txtAge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAge.KeyPress
        ' Allow only digits and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub rbMale_CheckedChanged(sender As Object, e As EventArgs) Handles rbMale.CheckedChanged

    End Sub
End Class
