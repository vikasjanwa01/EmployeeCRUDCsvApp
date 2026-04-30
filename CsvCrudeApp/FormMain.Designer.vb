<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dgvData = New DataGridView()
        btnAdd = New Button()
        btnEdit = New Button()
        btnDelete = New Button()
        CType(dgvData, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvData
        ' 
        dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvData.Location = New Point(0, 0)
        dgvData.Name = "dgvData"
        dgvData.ReadOnly = True
        dgvData.RowHeadersWidth = 62
        dgvData.Size = New Size(1027, 695)
        dgvData.TabIndex = 0
        ' 
        ' btnAdd
        ' 
        btnAdd.Location = New Point(71, 720)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(193, 67)
        btnAdd.TabIndex = 1
        btnAdd.Text = "Add"
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' btnEdit
        ' 
        btnEdit.Location = New Point(386, 720)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New Size(193, 67)
        btnEdit.TabIndex = 2
        btnEdit.Text = "Edit"
        btnEdit.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(712, 720)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(193, 67)
        btnDelete.TabIndex = 3
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' FormMain
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        ClientSize = New Size(1026, 821)
        Controls.Add(btnDelete)
        Controls.Add(btnEdit)
        Controls.Add(btnAdd)
        Controls.Add(dgvData)
        Name = "FormMain"
        Text = "Form1"
        CType(dgvData, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvData As DataGridView
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnDelete As Button

End Class
