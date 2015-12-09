Public Class frmQLSV
    Private _DBAccess As New DataBaseAccess
    Private _isLoading As Boolean = False

    Private Sub LoadDataOnComBobox()
        Dim sqlQuery As String = "SELECT ClassID, ClassName FROM dbo.Class"
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.cmblop.DataSource = dTable
        Me.cmblop.ValueMember = "ClassID"
        Me.cmblop.DisplayMember = "ClassName"
    End Sub


    Private Sub LoadDataOnGridView(ClassID As String)
        Dim sqlQuery As String = String.Format("SELECT StudentID, StudentName, Phone, Address FROM dbo.Students WHERE ClassID = '{0}'", ClassID)
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvsinhvien.DataSource = dTable
        With Me.dgvsinhvien
            .Columns(0).HeaderText = "StudentID"
            .Columns(1).HeaderText = "StudentName"
            .Columns(2).HeaderText = "Phone"
            .Columns(3).HeaderText = "Address"
        End With
    End Sub


    Private Sub frmQLSV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _isLoading = True
        LoadDataOnComBobox()
        LoadDataOnGridView(Me.cmblop.SelectedValue)
        _isLoading = False
    End Sub

    Private Sub cmblop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmblop.SelectedIndexChanged
        If Not _isLoading Then
            LoadDataOnGridView(Me.cmblop.SelectedValue)
        End If
    End Sub

    Private Sub SearchStudent(ClassID As String, value As String)
        Dim sqlQuery As String = String.Format("SELECT StudentID, StudentName, Phone, Address FROM dbo.Students WHERE ClassID = '{0}'", ClassID)
        If Me.cmbsearch.SelectedIndex = 0 Then
            sqlQuery += String.Format("AND StudentID LIKE '{0}%'", value)
        ElseIf Me.cmbsearch.SelectedIndex = 1 Then
            sqlQuery += String.Format("AND StudentName LIKE '{0}%'", value)
        End If
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvsinhvien.DataSource = dTable
        With Me.dgvsinhvien
            .Columns(0).HeaderText = "StudentID"
            .Columns(1).HeaderText = "StudentName"
            .Columns(2).HeaderText = "Phone"
            .Columns(3).HeaderText = "Address"
        End With
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        SearchStudent(Me.cmblop.SelectedValue, Me.txtsearch.Text)
    End Sub

    Private Sub btnthem_Click(sender As Object, e As EventArgs) Handles btnthem.Click
        Dim frm As New frmsv(False)
        frm.txtClassID.ReadOnly = True
        frm.txtClassID.Text = Me.cmblop.SelectedValue
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            LoadDataOnGridView(Me.cmblop.SelectedValue)
        End If
    End Sub

    Private Sub btnsua_Click(sender As Object, e As EventArgs) Handles btnsua.Click
        Dim frm As New frmsv(True)
        frm.txtClassID.Text = Me.cmblop.SelectedValue
        frm.txtStudentID.ReadOnly = True
        frm.txtClassID.ReadOnly = True
        With Me.dgvsinhvien
            frm.txtStudentID.Text = .Rows(.CurrentCell.RowIndex).Cells("StudentID").Value
            frm.txtStudentName.Text = .Rows(.CurrentCell.RowIndex).Cells("StudentName").Value
            frm.txtPhone.Text = .Rows(.CurrentCell.RowIndex).Cells("Phone").Value
            frm.txtAddress.Text = .Rows(.CurrentCell.RowIndex).Cells("Address").Value
        End With
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            LoadDataOnGridView(Me.cmblop.SelectedValue)
        End If
    End Sub

    Private Sub btnxoa_Click(sender As Object, e As EventArgs) Handles btnxoa.Click
        Dim StudentID As String = Me.dgvsinhvien.Rows(Me.dgvsinhvien.CurrentCell.RowIndex).Cells("StudentID").Value
        Dim sqlQuery As String = String.Format("delete Students where StudentID = '{0}'", StudentID)
        If _DBAccess.ExecuteNoneQuery(sqlQuery) Then
            MessageBox.Show("Xoá Thành Công Dự Liệu")
            LoadDataOnGridView(Me.cmblop.SelectedValue)
        Else
            MessageBox.Show("Lỗi Rồi Chưa Xóa Được Dữ Liệu")
        End If

    End Sub
End Class
