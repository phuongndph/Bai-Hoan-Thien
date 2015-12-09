Public Class frmsv

    Private _DBAccess As New DataBaseAccess
    Private _isEdit As Boolean = False

    Private Function ThemStudent() As Boolean
        Dim sqlQuery As String = "INSERT INTO Students (StudentID, StudentName, Phone, Address, ClassID)"
        sqlQuery += String.Format("VALUES('{0}','{1}','{2}','{3}','{4}')", _
                                  txtStudentID.Text, txtStudentName.Text, txtPhone.Text, txtAddress.Text, txtClassID.Text)
        Return _DBAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    Private Function IsEmpty() As Boolean
        Return (String.IsNullOrEmpty(txtStudentID.Text) OrElse _
                String.IsNullOrEmpty(txtStudentName.Text) OrElse _
                String.IsNullOrEmpty(txtPhone.Text) OrElse _
                String.IsNullOrEmpty(txtAddress.Text) OrElse _
                String.IsNullOrEmpty(txtClassID.Text))
    End Function

    Private Function SuaStudent() As Boolean
        Dim sqlQuery As String = String.Format("UPDATE Students SET StudentName = '{0}', Phone ='{1}', Address ='{2}' WHERE StudentID ='{3}'", _
        Me.txtStudentName.Text, Me.txtPhone.Text, Me.txtAddress.Text, Me.txtStudentID.Text)

        Return _DBAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        If IsEmpty() Then
            MessageBox.Show("Hãy Nhập Giá Trị Vào Trước Khi Ghi Vào DataBase", "Error", MessageBoxButtons.OK)
        Else
            If _isEdit Then
                If SuaStudent() Then
                    MessageBox.Show("Đã Sửa Dữ Liệu Thành Công", "Thông Báo", MessageBoxButtons.OK)

                Else
                    MessageBox.Show("Lỗi Rồi Chưa Sửa Được Dữ Liệu Đâu Nhé", "Thông Báo", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            Else
                If ThemStudent() Then
                    MessageBox.Show("Ô Kê Men! Thêm Dữ Liệu Thành Công", "Thông Báo", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Có Lỗi Gì Đó Rồi Chưa Nhập Được Dữ Liệu Đâu", "Thông Báo", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If

                End If
                Me.Close()
        End If
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub

    Public Sub New(Isedit As Boolean)
        InitializeComponent()
        _isEdit = Isedit
    End Sub
End Class