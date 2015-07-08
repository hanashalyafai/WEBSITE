<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" Trace="true" AutoEventWireup="true" CodeBehind="addCommand.aspx.cs" Inherits="home.WEB.addCommand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div>
            <asp:GridView ID="gvAppliances" CssClass="centralizeDiv text-capitalize  text-center" OnRowDataBound="gvAppliances_RowDataBound" OnRowCommand="gvAppliances_RowCommand" OnRowUpdating="gvAppliances_RowUpdating" OnPageIndexChanging="gvAppliances_PageIndexChanging" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="5">
                <Columns>
                    <asp:BoundField DataField="name" HeaderText="Name" />
                    <asp:BoundField DataField="state" HeaderText="State" />
                    <asp:BoundField HeaderText="Year" SortExpression="" />
                    <asp:BoundField HeaderText="Month" />
                    <asp:BoundField HeaderText="Day" />
                    <asp:BoundField HeaderText="Hour" />
                    <asp:BoundField HeaderText="Minute" />
                    <asp:BoundField HeaderText="Second" />
                    <asp:TemplateField HeaderText="Option">
                        <ItemTemplate>
                            <input id="currentTime" type="button" onclick="currentTimeClicked(this)" value="Current Time"></input>
                            <input id="add" type="button" onclick="addClicked(this)" value=" Add "></input>
                            <asp:HiddenField runat="server" ID="hfID" Value='<%#Eval("ApplianceID")%>' />
                            <asp:HiddenField runat="server" ID="hfPin" Value='<%#Eval("pin")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <input type="button" id="selectedCurrentTime" style="visibility: hidden; border: 0;" />
        <input type="button" id="selectedAdd" style="visibility: hidden; border: 0;" />
    </form>
    <script type="text/javascript">
        var rowIndex;


        function getIndex(lnk) {
            //alert("helllllllllllllllllllo ");

            var row = lnk.parentNode.parentNode;
            rowIndex = row.rowIndex - 1;

            //alert(rowIndex);

        };
        function currentTimeClicked(obj) {
            getIndex(obj);
            selectedCurrentTime.click();

        };
        function addClicked(obj) {
            getIndex(obj);
            selectedAdd.click();
        }
        $(document).ready(function () {


            $("#selectedCurrentTime").click(function () {
                $.ajax({
                    type: "POST",
                    url: "addCommand.aspx/getCurrentTime",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //   alert(msg.d);
                        var parsed = $.parseJSON(msg.d);
                        $('#ContentPlaceHolder1_gvAppliances_yearDDL_' + rowIndex + ' :selected').text(parsed.year);
                        $('#ContentPlaceHolder1_gvAppliances_monthDDL_' + rowIndex + ' :selected').text(parsed.month);
                        $('#ContentPlaceHolder1_gvAppliances_dayDDL_' + rowIndex + ' :selected').text(parsed.day);
                        $('#ContentPlaceHolder1_gvAppliances_hourDDL_' + rowIndex + ' :selected').text(parsed.hour);
                        $('#ContentPlaceHolder1_gvAppliances_minuteDDL_' + rowIndex + ' :selected').text(parsed.minute);
                        $('#ContentPlaceHolder1_gvAppliances_secondDDL_' + rowIndex + ' :selected').text(parsed.second);


                    },
                    error: function () {
                        alert("failure!!");
                    }
                });
            });

            $("#selectedAdd").click(function () {
                var year = $('#ContentPlaceHolder1_gvAppliances_yearDDL_' + rowIndex + ' :selected').text();
                var month = $('#ContentPlaceHolder1_gvAppliances_monthDDL_' + rowIndex + ' :selected').text();
                var day = $('#ContentPlaceHolder1_gvAppliances_dayDDL_' + rowIndex + ' :selected').text();
                var hour = $('#ContentPlaceHolder1_gvAppliances_hourDDL_' + rowIndex + ' :selected').text();
                var minute = $('#ContentPlaceHolder1_gvAppliances_minuteDDL_' + rowIndex + ' :selected').text();
                var second = $('#ContentPlaceHolder1_gvAppliances_secondDDL_' + rowIndex + ' :selected').text();
                var state = $('#ContentPlaceHolder1_gvAppliances_stateDDL_' + rowIndex + ' :selected').text();
                var ID = $('#ContentPlaceHolder1_gvAppliances_hfID_' + rowIndex).val();
                var pin = $('#ContentPlaceHolder1_gvAppliances_hfPin_' + rowIndex).val();

                //alert(pin + " is pin");
                var myObj = '{ "ID": "' + ID + '","state": "' + state + '","pin": "' + pin + '","year": "' + year + '","month": "' + month + '","day": "' + day + '","hour": "' + hour + '","minute": "' + minute + '","second": "' + second + '" }';
               // alert(myObj);
                $.ajax({
                    type: "POST",
                    url: "/addCommand.aspx/addSelectedCommand",
                    //data:Obj,
                    data: myObj,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                      //  alert(msg.d + " succeeded!!");
                    },
                    error: function (msg) {
                        alert(msg.responseText + " failure!!");
                    }

                });
            });

        });
    </script>

</asp:Content>
