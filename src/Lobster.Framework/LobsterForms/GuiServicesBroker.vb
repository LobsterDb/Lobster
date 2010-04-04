'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class LobsterFormGuiServicesBroker
    Implements IServicesProxy

    Private _factories As IFactories
    Private _request As JsonProxy

    Sub New(ByVal factories As IFactories, ByVal request As JsonProxy)

        _factories = factories
        _request = request

    End Sub

    Public Function RouteCall() As JsonString _
        Implements IServicesProxy.RouteCall

        Dim methodName As String = _request.GetString("MethodName")
        Dim arguments As JsonProxy = _request.GetDataObject("Arguments")

        Dim className As String = arguments.GetString("ClassName")
        Dim classModel As ClassModel = _factories.GetClassModel(className)

        Dim target As New LobsterFormGuiServices()

        'target.GetFormItemsJS(classModel())

    End Function

End Class
