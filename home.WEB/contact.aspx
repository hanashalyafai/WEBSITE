<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="home.WEB.contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row">
            <div class="box">
                <div class="col-lg-12">
                    <hr>
                    <h2 class="intro-text text-center">Contact
                        <strong> Team Leader </strong>
                    </h2>
                    <hr>
                </div>
               <div class="col-md-8">
                   <!--   Image Herrrrrreeee -->
                   <img width="600" height="400"  src="images/nice.jpg" />
               </div>
                <div class="col-md-4">
                    <p>Phone:
                        <strong>+2-0100 793 1250</strong>
                    </p>
                    <p>Email:
                        <strong><a href="hanashalyafai@yahoo.com">hanashalyafai@yahoo.com </a></strong>
                    </p>
                    <p>Address:
                        <strong>Al-Faisal Street
                            <br>Cairo, Egypt</strong>
                    </p>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>

       <div class="row">
            <div class="box">
                <div class="col-lg-12">
                    <hr>
                    <h2 class="intro-text text-center">Contact
                        <strong>form</strong>
                    </h2>
                    <hr>
                    <p>
                       contact the system administrator  
                    </p>
                    <form id="form1" onsubmit="return false;" role="form">
                        <div class="row">
                            <div class="form-group col-lg-4">
                                <label>Name</label>
                                <input id="name" type="text" value="" class="form-control" required>
                            </div>
                            <div class="form-group col-lg-4">
                                <label>Email Address</label>
                                <input type="email" id="email" value="" class="form-control" required>
                            </div>
                            <div class="form-group col-lg-4">
                                <label>Phone Number</label>
                                <input id="phoneNum" type="tel" value="" class="form-control" required>
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group col-lg-12">
                                <label>Message</label>
                                <textarea id="msgTxt" class="form-control" aria-valuetext="" rows="6" required></textarea>
                            </div>
                            <div class="form-group col-lg-12">
                                <input id="ay7aga" type="hidden" name="save" value="contact">
                                <button type="submit" id="submit" value="ay7aga" class="btn btn-default" >Submit</button>
                                <label id="msgLabel" hidden="hidden" >sodfgnsgdjb</label>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

    <script type="text/javascript">
        
        $(document).ready(function () {
            $('#form1').submit(function (event) {
                var errors = 0;
                $("#form1 :input").map(function () {
                    if (!$(this).val()) {
                        $(this).parents('td').addClass('warning');
                        errors++;
                    } else if ($(this).val()) {
                        $(this).parents('td').removeClass('warning');
                    }
                });
               // alert(errors);

                if (errors > 0) {
                    $('#errorwarn').text("All fields are required");
                    return false;
                }

                // ajax..    
                var name = $("#name").val();
                var email = $("#email").val();
                var phoneNum = $("#phoneNum").val();
                var msgTxt = $("#msgTxt").val();
                
                var dataObjetJSON = '{"name":"' + name + '","email":"' + email + '","phoneNum":"' + phoneNum + '","msg":"' + msgTxt + '"}';

                $.ajax({
                    type: "POST",
                    url: "/contact.aspx/sendMessage",
                    data: dataObjetJSON,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        alert(x);
                        if (x) {
                            $("#msgLabel").text("your message was sent, thank youe!");
                            $("#msgLabel").show();
                            $("#name").val("");
                            $("#email").val("");
                            $("#phoneNum").val("");
                            $("#msgTxt").val("");
                        } else {
                            $("#msgLabel").text("your message was not sent .. Pleas, try again later!!");
                            $("#msgLabel").show();
                        }
                    },
                    error: function (msg) {
                        $("#msgLabel").text("your message was not sent .. Pleas, try again later!!");
                        $("#msgLabel").show();
                    }
                });
            }); 

        });

    </script>


</asp:Content>
