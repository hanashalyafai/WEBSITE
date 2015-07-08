
<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="secureMode.aspx.cs" Inherits="home.WEB.secureMode" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="col-md-6 text-left" >

        <input id="randomDayEnable" type="checkbox" />Secure Mode Enabled<br/>
        <input id="cameraEnable" onchange="enablePicVid();" type="checkbox" />Camera Mode Enabled (X- not yet!)<br/>
         &nbsp &nbsp &nbsp &nbsp<input  name="picVid" id="picturesEnable" type="radio" />Pictures Enabled (X- not yet!)<br/>
         &nbsp &nbsp &nbsp &nbsp<input name="picVid" id="videoEnable" type="radio" />Video Streaming Enabled   (X- not yet!)<br/>
        <input id="motionDetectionEnable" type="checkbox" />Motion Detection Mode Enabled (X- not yet!)<br/>
        

    </div>

    <div id="imgContainer" class="col-md-6">
        <!--  -->
        <img id="houseImg" class="img-responsive img-border-left" src="images/nice.jpg" alt="">
    </div>


    <script type="text/javascript">
        var isEnabledPicVid;
        function enablePicVid() {
            isEnabledPicVid = !isEnabledPicVid;
            
            $("input[type=radio]").attr('disabled', isEnabledPicVid);
            if (isEnabledPicVid) {
                $('#picturesEnable').attr('checked', false);
                $('#videoEnable').attr('checked', false);
            }

        };
        $(document).ready(function () {
            isEnabledPicVid = true;
            $("input[type=radio]").attr('disabled', isEnabledPicVid);
        });



        $("#randomDayEnable").change(function () {
            /*
            $.ajax({
                type: "GET",
                url: "img/slide-2.jpg",
                dataType: "image/jpg",
                success: function (msg) {
                    if(msg.d=='true')
                        $('#houseImg').attr('src','/img/slide-2.jpg');
                    
                },
                error: function (error, txtStatus) {
                    alert("failed to change image!!!");
                }
            });
            
            */
            $.ajax({
                type: "POST",
                url: "secureMode.aspx/randomDay",
                data: '{"isEnabled":' + $('#randomDayEnable').prop('checked') + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                    // alert("success");

                },
                error: function () {
                    //  alert("failure!!");
                }
            });


        });


        $("#picturesEnable").change(function () {
            $.ajax({
                type: "POST",
                url: "secureMode.aspx/pictureStreaming",
                data: '{"isEnabled":' + $('#picturesEnable').prop('checked') + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                },
                error: function () {
                }
            });
        });

        $("#motionDetectionEnable").change(function () {
            $.ajax({
                type: "POST",
                url: "secureMode.aspx/motionDetection",
                data: '{"isEnabled":' + $('#motionDetectionEnable').prop('checked') + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                },
                error: function () {
                }
            });
        });

        $("#videoEnable").change(function () {

            $.ajax({
                type: "POST",
                url: "secureMode.aspx/videoStreaming",
                data: '{"isEnabled":' + $('#videoEnable').prop('checked') + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                   // alert("success");

                },
                error: function () {
                  //  alert("failure!!");
                }
            });
        });

   </script>


</asp:Content>
