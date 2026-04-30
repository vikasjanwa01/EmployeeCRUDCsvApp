Imports System.IO

Public Class FormMain

    Dim filePath As String = Application.StartupPath & "\data.csv"
    Dim addedFilePath As String = Application.StartupPath & "\added.csv"
    Dim deletedFilePath As String = Application.StartupPath & "\deleted.csv"
    Dim dt As New DataTable()

    ' ================= FORM LOAD =================
    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Set up columns only once here
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name")
        dt.Columns.Add("Age")
        dt.Columns.Add("Gender")
        dt.Columns.Add("Department")
        dt.Columns.Add("EmploymentType")

        dgvData.DataSource = dt  ' ✅ Bind once only

        LoadCSV()
    End Sub

    'Create SaveAddedCSV data 

    Private Sub SaveAddedCSV(data As String())

        Dim fileExists As Boolean = File.Exists(addedFilePath)

        Using sw As New StreamWriter(addedFilePath, True)

            ' Write header only once
            If Not fileExists Then
                sw.WriteLine("ID,Name,Age,Gender,Department,EmploymentType")
            End If

            sw.WriteLine(String.Join(",", data))
        End Using

    End Sub


    'Create SaveDelete data method 

    Private Sub SaveDeletedCSV(data As String())

        Dim deletedFilePath As String = Application.StartupPath & "\deleted.csv"
        Dim fileExists As Boolean = IO.File.Exists(deletedFilePath)

        Using sw As New IO.StreamWriter(deletedFilePath, True)

            ' Write header only once
            If Not fileExists Then
                sw.WriteLine("ID,Name,Age,Gender,Department,EmploymentType")
            End If

            sw.WriteLine(String.Join(",", data))
        End Using

    End Sub


    ' ================= LOAD CSV =================
    Private Sub LoadCSV()

        dt.Rows.Clear()   ' ✅ Clear ROWS only — never touch columns

        If Not File.Exists(filePath) Then
            MessageBox.Show("CSV file not found")
            Exit Sub
        End If

        Dim lines = File.ReadAllLines(filePath)

        For i As Integer = 1 To lines.Length - 1

            Dim line As String = lines(i).Trim()
            If String.IsNullOrWhiteSpace(line) Then Continue For

            Dim parts = line.Split(","c)
            If parts Is Nothing OrElse parts.Length < 6 Then Continue For
            If String.IsNullOrWhiteSpace(parts(0)) Then Continue For

            Dim idVal As Integer
            If Not Integer.TryParse(parts(0).Trim(), idVal) Then Continue For

            ' ✅ Wrap in try-catch to skip any bad rows safely
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
                ' Skip malformed rows silently
                Continue For
            End Try

        Next

        dt.DefaultView.Sort = "ID ASC"
        ' ✅ Do NOT re-assign DataSource here — binding already set in Load
        dgvData.ClearSelection()

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

        If frm.ShowDialog() = DialogResult.OK Then

            If frm.resultData Is Nothing Then
                MessageBox.Show("No data returned from FormEdit")
                Exit Sub
            End If

            ' ✅ Add to DataTable (NOT directly to CSV)
            dt.Rows.Add(
            Convert.ToInt32(frm.resultData(0)),
            frm.resultData(1),
            frm.resultData(2),
            ConvertToShort(frm.resultData(3)),
            frm.resultData(4),
            frm.resultData(5)
)

            ' ✅ Save everything
            SaveAllCSV()
            LoadCSV()

            ' ✅ Log added data
            SaveAddedCSV(frm.resultData)


        End If

    End Sub

    ' ================= EDIT =================
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        If dgvData.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a row")
            Exit Sub
        End If

        Dim frm As New FormEdit()

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

        If dgvData.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a row")
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show(
        "Do you want to delete this record?",
        "Confirm Delete",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    )

        If result = DialogResult.No Then Exit Sub
        Dim idToDelete As String = dgvData.CurrentRow.Cells("ID").Value.ToString()
        'Dim idToDelete As String = dgvData.CurrentRow.Cells("ID").Value.ToString()

        ' ✅ Find correct row in dt
        For i As Integer = 0 To dt.Rows.Count - 1

            If dt.Rows(i)("ID").ToString() = idToDelete Then

                ' ✅ Save before delete
                Dim deletedData As String() = {
                dt.Rows(i)("ID").ToString(),
                dt.Rows(i)("Name").ToString(),
                dt.Rows(i)("Age").ToString(),
                ConvertToFull(dt.Rows(i)("Gender").ToString()),
                dt.Rows(i)("Department").ToString(),
                dt.Rows(i)("EmploymentType").ToString()
            }

                SaveDeletedCSV(deletedData)

                ' ✅ Remove
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

End Class