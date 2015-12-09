<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxy.aspx.cs" Inherits="ajaxy" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">



<head runat="server">
    <title></title>




     <script type="text/javascript" src="JS/jquery-1.7.2.js"></script>
    
    <script type="text/javascript">

        function CallServerAdd() {
            var a = document.getElementById('TextBox1').value;
            var b = document.getElementById('TextBox2').value;
            PageMethods.Add(a, b, OnResult, OnError);
        }
        
        function OnResult(result, userContext, method) {
            var resultMsg = "Result from Method [" + method + "] is: " + result;
            alert(resultMsg);
        }


        function CallServerMethod() {
            
            PageMethods.GetMessage(OnSuccess);
        }
        function OnSuccess(result) {
            alert(result);
        }

        //kiedy DOkument jest ready wywowaj onready
        $(document).ready(OnReady);

        function OnReady() {
            
            $("#drpContinent").change(onChange);
        }

        function onChange() {
            //create the ajax request
            $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "ajaxy.aspx/OnContinentChange", 
                    data: "{'continentName':'" + $('#drpContinent').val() + "'}", //json to represent argument
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: callback,
                    error: onError
                }
            );

        }

        //Handle the callback on success
        function callback(msg) {
            //obsługa wywołania wzzrotnego              
            //request was successful.
            var countries = msg.split(';');
            var length = countries.length;

            //Change the second dropdownlists items
            
            document.getElementById('<#=drpCountry.ClientID #>').options.length = 0;

        // add the new items to the dropdown.
        var dropDown = document.getElementById('<#=drpCountry.ClientID #>');
        for (var i = 0; i < length - 1; ++i) {
            var option = document.createElement("option");
            option.text = countries[i];
            option.value = countries[i];

            dropDown.options.add(option);
        }
    }

    
    function onError() {
        alert('cus poszło nie tak');
    }

    </script>
    

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
    
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="Oblicz"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Text="A: "></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBox1_TextBoxWatermarkExtender" runat="server" BehaviorID="TextBox1_TextBoxWatermarkExtender" TargetControlID="TextBox1" WatermarkText="podaj liczbę" />
        <br />
        <asp:Label ID="Label3" runat="server" Text="B: "></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
&nbsp;<br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Oblicz" OnClientClick="CallServerAdd(); return false;" />
                <br />
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                       Proszę czekać...
                        </ProgressTemplate>
                </asp:UpdateProgress>
                <br />
        <br />
        <asp:Label ID="Label4" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server"></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
                </asp:Timer>
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label7" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="Label8" runat="server"></asp:Label>
        
        <br />
                <asp:DropDownList ID="drpContinent" runat="server">
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="drpCountry" runat="server">
                </asp:DropDownList>
        <ajaxToolkit:DropDownExtender ID="drpCountry_DropDownExtender" runat="server" BehaviorID="drpCountry_DropDownExtender" DynamicServicePath="" TargetControlID="drpCountry">
        </ajaxToolkit:DropDownExtender>
        <br />
                <asp:Button ID="Button2" runat="server" Text="Button" OnClientClick="CallServerMethod(); return false;"  />
        </div>
        <p>
            <asp:Button ID="Button3" runat="server" Text="Button" />
            <ajaxToolkit:ConfirmButtonExtender ID="Button3_ConfirmButtonExtender" runat="server" BehaviorID="Button3_ConfirmButtonExtender" ConfirmText="jakaś wiadomość" TargetControlID="Button3" />
        </p>
        <ajaxToolkit:ComboBox ID="ComboBox1" runat="server">
            <asp:ListItem>a</asp:ListItem>
            <asp:ListItem>b</asp:ListItem>
            <asp:ListItem>c</asp:ListItem>
        </ajaxToolkit:ComboBox>
    </form>
</body>
</html>


