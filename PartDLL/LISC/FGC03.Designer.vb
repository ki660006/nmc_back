﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGC03
    Inherits LISC.FGC01

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'FGC03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1162, 656)
        Me.Name = "FGC03"
        Me.Text = "수탁 채혈"
        Me.ResumeLayout(False)
        '
        'lblLineQry
        '
        Me.lblLineQry.Visible = False
        '
        'grpList
        '
        Me.grpList.Visible = False
        '
        'lblOrderInfo
        '
        Me.lblOrderInfo.Left = 3
        Me.lblOrderInfo.Width += Me.grpList.Width + 10
        '
        'axCollList
        '
        Me.axCollList.Left = 3
        Me.axCollList.Width += Me.grpList.Width + 10
        '
        'axCollBcNos
        '
        Me.axCollBcNos.Left = 3
        Me.axCollBcNos.Width += Me.axCollBcNos.Width + 10

    End Sub
End Class
