Imports System.IO

Public Class FormMain

    Dim filePath As String = Application.StartupPath & "\data.csv"
    Dim addedFilePath As String = Application.StartupPath & "\added.csv"
    Dim deletedFilePath As String = Application.StartupPath & "\deleted.csv"
    Dim dt As New DataTable()
    Dim selectedRowID As Integer = -1

    ' ================= FORM LOAD =================
    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name")
        dt.Columns.Add("Age")
        dt.Columns.Add("Gender")
        dt.Columns.Add("Department")
        dt.Columns.Add("EmploymentType")

        dgvData.DataSource = dt

        LoadCSV()

        dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvData.ClearSelection()
        dgvData.AllowUserToAddRows = False

        ' ✅ Name and Age sortable
        dgvData.Columns("Name").SortMode = DataGridViewColumnSortMode.Automatic
        dgvData.Columns("Age").SortMode = DataGridViewColumnSortMode.Automatic

        ' ✅ Disable sorting for other 3 columns
        dgvData.Columns("Gender").SortMode = DataGridViewColumnSortMode.NotSortable
        dgvData.Columns("Department").SortMode = DataGridViewColumnSortMode.NotSortable
        dgvData.Columns("EmploymentType").SortMode = DataGridViewColumnSortMode.NotSortable

        ' ✅ Header color
        dgvData.EnableHeadersVisualStyles = False
        dgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        dgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvData.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold)

        ' ✅ Fill columns to full width
        dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

    End Sub

    ' ================= SAVE ADDED CSV =================
    Private Sub SaveAddedCSV(data As String())

        Dim fileExists As Boolean = File.Exists(addedFilePath)

        Using sw As New StreamWriter(addedFilePath, True)
            If Not fileExists Then
                sw.WriteLine("ID,Name,Age,Gender,Department,EmploymentType")
            End If
            sw.WriteLine(String.Join(",", data))
        End Using

    End Sub

    ' ================= SAVE DELETED CSV =================
    Private Sub SaveDeletedCSV(data As String())

        Dim fileExists As Boolean = IO.File.Exists(deletedFilePath)

        Using sw As New IO.StreamWriter(deletedFilePath, True)
            If Not fileExists Then
                sw.WriteLine("ID,Name,Age,Gender,Department,EmploymentType")
            End If
            sw.WriteLine(String.Join(",", data))
        End Using

    End Sub

    ' ================= LOAD CSV =================
    Private Sub LoadCSV()

        dt.Rows.Clear()

        If Not File.Exists(filePath) Then
            MessageBox.Show("CSV file not found")
            Exit Sub
        End If

        Dim lines = File.ReadAllLines(filePath)
        Dim seenIDs As New HashSet(Of Integer)()
        Dim duplicateIDs As New List(Of String)()

        For i As Integer = 1 To lines.Length - 1

            Dim line As String = lines(i).Trim()
            If String.IsNullOrWhiteSpace(line) Then Continue For

            Dim parts = line.Split(","c)
            If parts Is Nothing OrElse parts.Length < 6 Then Continue For
            If String.IsNullOrWhiteSpace(parts(0)) Then Continue For

            Dim idVal As Integer
            If Not Integer.TryParse(parts(0).Trim(), idVal) Then Continue For

            If seenIDs.Contains(idVal) Then
                duplicateIDs.Add(idVal.ToString())
                Continue For
            End If

            seenIDs.Add(idVal)

            Try
                dt.Rows.Add(
                    idVal,
                    parts(1).Trim(),
                    parts(2).Trim(),
                    ConvertToShort(parts(3).Trim()),
                    parts(4).Trim(),
                    parts(5).Trim()
                )
            Catch ex As Exception
                Continue For
            End Try

        Next

        If duplicateIDs.Count > 0 Then
            MessageBox.Show(
                "Duplicate ID(s) found Remove Duplicates :" & Environment.NewLine &
                String.Join(", ", duplicateIDs),
                "Duplicate ID Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )
            Me.Close()
            Exit Sub
        End If

        ' ✅ Sort by ID without showing arrow
        dgvData.Columns("ID").SortMode = DataGridViewColumnSortMode.NotSortable
        dt.DefaultView.Sort = "ID ASC"
        dgvData.Columns("ID").SortMode = DataGridViewColumnSortMode.Automatic

        dgvData.ClearSelection()
        SaveAllCSV()

    End Sub

    ' ================= CONVERSION =================
    Private Function ConvertToShort(full As String) As String
        If full = "Male" Then Return "M"
        If full = "Female" Then Return "F"
        Return ""
    End Function

    Private Function ConvertToFull(shortVal As String) As String
        If shortVal = "M" Then Return "Male"
        If shortVal = "F" Then Return "Female"
        Return ""
    End Function

    ' ================= ADD =================
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim frm As New FormEdit()
        frm.Text = "Add New Employee"

        If frm.ShowDialog() = DialogResult.OK Then

            If frm.resultData Is Nothing Then
                MessageBox.Show("No data returned from FormEdit")
                Exit Sub
            End If

            dt.Rows.Add(
                Convert.ToInt32(frm.resultData(0)),
                frm.resultData(1),
                frm.resultData(2),
                ConvertToShort(frm.resultData(3)),
                frm.resultData(4),
                frm.resultData(5)
            )

            SaveAllCSV()
            LoadCSV()
            SaveAddedCSV(frm.resultData)

        End If

    End Sub

    ' ================= EDIT =================
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        If dgvData.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a row")
            Exit Sub
        End If

        Dim frm As New FormEdit()
        frm.Text = "Edit Employee Details"

        Dim genderValue = dgvData.CurrentRow.Cells("Gender").Value
        Dim genderFull = ConvertToFull(If(genderValue IsNot Nothing, genderValue.ToString(), ""))

        frm.SetData(
            dgvData.CurrentRow.Cells("ID").Value.ToString(),
            dgvData.CurrentRow.Cells("Name").Value.ToString(),
            dgvData.CurrentRow.Cells("Age").Value.ToString(),
            genderFull,
            dgvData.CurrentRow.Cells("Department").Value.ToString(),
            dgvData.CurrentRow.Cells("EmploymentType").Value.ToString()
        )

        If frm.ShowDialog() = DialogResult.OK Then

            Dim idToEdit As String = dgvData.CurrentRow.Cells("ID").Value.ToString()

            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("ID").ToString() = idToEdit Then
                    dt.Rows(i)("ID") = Convert.ToInt32(frm.resultData(0))
                    dt.Rows(i)("Name") = frm.resultData(1)
                    dt.Rows(i)("Age") = frm.resultData(2)
                    dt.Rows(i)("Gender") = ConvertToShort(frm.resultData(3))
                    dt.Rows(i)("Department") = frm.resultData(4)
                    dt.Rows(i)("EmploymentType") = frm.resultData(5)
                    Exit For
                End If
            Next

            SaveAllCSV()
            LoadCSV()
        End If

    End Sub

    ' ================= DELETE =================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If dgvData.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a row")
            Exit Sub
        End If

        ' ✅ Custom confirm dialog
        Dim frm As New FormConfirm("Do you want to delete this record?")
        frm.ShowDialog(Me)
        If frm.UserChoice = False Then Exit Sub

        Dim idToDelete As String = dgvData.CurrentRow.Cells("ID").Value.ToString()

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("ID").ToString() = idToDelete Then

                Dim deletedData As String() = {
                    dt.Rows(i)("ID").ToString(),
                    dt.Rows(i)("Name").ToString(),
                    dt.Rows(i)("Age").ToString(),
                    ConvertToFull(dt.Rows(i)("Gender").ToString()),
                    dt.Rows(i)("Department").ToString(),
                    dt.Rows(i)("EmploymentType").ToString()
                }

                SaveDeletedCSV(deletedData)
                dt.Rows.RemoveAt(i)
                Exit For
            End If
        Next

        SaveAllCSV()
        LoadCSV()

    End Sub

    ' ================= SAVE NEW =================
    Private Sub SaveToCSV(data As String())
        Using sw As New StreamWriter(filePath, True)
            sw.WriteLine(String.Join(",", data))
        End Using
    End Sub

    ' ================= SAVE ALL =================
    Private Sub SaveAllCSV()

        Using sw As New StreamWriter(filePath, False)
            sw.WriteLine("ID,Name,Age,Gender,Department,EmploymentType")
            For Each row As DataRow In dt.Rows
                sw.WriteLine(String.Join(",", {
                    row("ID").ToString(),
                    row("Name").ToString(),
                    row("Age").ToString(),
                    ConvertToFull(row("Gender").ToString()),
                    row("Department").ToString(),
                    row("EmploymentType").ToString()
                }))
            Next
        End Using

    End Sub

    ' ================= COLUMN HEADER CLICK =================
    Private Sub dgvData_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvData.ColumnHeaderMouseClick

        If dgvData.SelectedRows.Count > 0 Then
            selectedRowID = Convert.ToInt32(dgvData.SelectedRows(0).Cells("ID").Value)
        Else
            selectedRowID = -1
        End If

        Dim colName As String = dgvData.Columns(e.ColumnIndex).Name

        If colName <> "ID" AndAlso colName <> "Name" AndAlso colName <> "Age" Then
            Exit Sub
        End If

        If dt.DefaultView.Sort = colName & " ASC" Then
            dt.DefaultView.Sort = colName & " DESC"
        Else
            dt.DefaultView.Sort = colName & " ASC"
        End If

        dgvData.ClearSelection()

        If selectedRowID = -1 Then Exit Sub

        For Each row As DataGridViewRow In dgvData.Rows
            If row.Cells("ID").Value IsNot Nothing Then
                If Convert.ToInt32(row.Cells("ID").Value) = selectedRowID Then
                    dgvData.CurrentCell = row.Cells(0)
                    row.Selected = True
                    Exit For
                End If
            End If
        Next

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub

End Class

' ================= CUSTOM CONFIRM DIALOG =================
Public Class FormConfirm
    Inherits Form

    Public Property UserChoice As Boolean = False
    Private lblMessage As New Label()
    Private btnYes As New Button()
    Private btnNo As New Button()

    Public Sub New(message As String)
        Me.Text = "Confirm"
        Me.Size = New Size(320, 160)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        lblMessage.Text = message
        lblMessage.Size = New Size(280, 40)
        lblMessage.Location = New Point(15, 20)
        lblMessage.TextAlign = ContentAlignment.MiddleCenter

        btnYes.Text = "Yes"
        btnYes.Size = New Size(80, 30)
        btnYes.Location = New Point(60, 75)

        btnNo.Text = "No"
        btnNo.Size = New Size(80, 30)
        btnNo.Location = New Point(160, 75)

        Me.Controls.Add(lblMessage)
        Me.Controls.Add(btnYes)
        Me.Controls.Add(btnNo)

        AddHandler btnYes.Click, AddressOf btnYes_Click
        AddHandler btnNo.Click, AddressOf btnNo_Click
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs)
        UserChoice = True
        Me.Close()
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs)
        UserChoice = False
        Me.Close()
    End Sub

End Class
