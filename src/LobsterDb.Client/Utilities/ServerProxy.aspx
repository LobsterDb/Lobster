<%@ Page Language="VB" %>

<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim requestString As String = Request.Form("Request")
        Dim json As String = RouteCall(requestString)
        Response.Write(json)
    End Sub
    
    Public Function RouteCall(ByVal requestString As String) As String

        Dim request As New Lobster.Framework.JsonProxy(requestString)
        Dim proxy As Lobster.Framework.IServicesProxy

        Select Case request.GetString("ClassName")
            Case Is = "StateGraph"
                proxy = New Lobster.Framework.StateGraphBroker(request)
            Case Is = "CompositeSet"
                proxy = New Lobster.Framework.ClientServices.CompositeSetBroker(request)
            Case Is = "ClassModel"
                proxy = New Lobster.Framework.ClassModelBroker(request)
            Case Is = "ClassModelReader"
                proxy = New Lobster.Framework.ClientServices.ClassModelReader(request)
            Case Is = "Composite"
                proxy = New Lobster.Framework.CompositeBroker(request)
            Case Else
                Throw New Exception("ServerProxy.aspx")
        End Select

        Dim returnObject As Lobster.Framework.JsonString = proxy.RouteCall()
        Dim returnString As String = returnObject.GetJson()

        Return returnString

    End Function
     
    
</script>