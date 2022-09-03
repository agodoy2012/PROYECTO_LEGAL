<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SEGUIMIENTO_LEGAL.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
    <link href="~/Img/logoGC.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
   <%-- <link href="Content/login_main.css" rel="stylesheet" />
    <link href="Content/login_util.css" rel="stylesheet" />--%>
    <%--<link href="Content/icon-font.min.css" rel="stylesheet"/>--%>

<!--===============================================================================================-->
	<link href="Scripts/LoginCss/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="Scripts/LoginCss/fonts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="Scripts/LoginCss/fonts/iconic/css/material-design-iconic-font.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="Scripts/LoginCss/vendor/animate/animate.css" rel="stylesheet" />
<!--===============================================================================================-->	
    <link href="Scripts/LoginCss/vendor/css-hamburgers/hamburgers.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="Scripts/LoginCss/vendor/animsition/css/animsition.min.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="Scripts/LoginCss/vendor/select2/select2.min.css" rel="stylesheet" />
<!--===============================================================================================-->	
    <link href="Scripts/LoginCss/vendor/daterangepicker/daterangepicker.css" rel="stylesheet" />
<!--===============================================================================================-->
    <link href="Scripts/LoginCss/css/util.css" rel="stylesheet" />
    <link href="Scripts/LoginCss/css/main.css" rel="stylesheet" />
<!--===============================================================================================-->
   <link href="https://fonts.googleapis.com/css?family=Raleway:500" rel="stylesheet"/>

    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <title>Inicio de Sesión</title>
</head>
<body class="hold-transition login-page">

  <%-- <div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100 p-l-85 p-r-85 p-t-55 p-b-55">
				<form runat="server" class="login100-form validate-form flex-sb flex-w" autocomplete="off">

                    <span class="login100-form-title p-b-32" style="color:#B40404">
						Área Legal Guatemala
					</span>

					<span class="txt1 p-b-11">
						Nombre de Usuario
					</span>
					<div class="wrap-input100 validate-input m-b-36" data-validate = "El nombre de usuario es requerido">
                        
						<input class="input100" type="text" runat="server" name="username" id="username" />
						<span class="focus-input100"></span>
					</div>
					
					<span class="txt1 p-b-11">
						Constraseña
					</span>
					<div class="wrap-input100 validate-input m-b-12" data-validate = "La contraseña es requerida">
				
						<input class="input100" type="password" runat="server" name="pass" id="pass" />
						<span class="focus-input100"></span>
					</div>

					<div class="container-login100-form-btn">

                        <asp:Button ID="btn_ingreso" class="login100-form-btn" ClientIDMode="Static" runat="server" Text="Ingresar"/>

					</div>

				</form>
			</div>
		</div>
	</div>--%>
    <div class="limiter">
		<div class="container-login100" style="background-image: url('Scripts/LoginCss/images/fondo1.jpg');">

			<div class="wrap-login100">
					<form runat="server" class="login100-form validate-form flex-sb flex-w" autocomplete="off">
					<span class="login100-form-title p-b-26">
						Área Legal Guatemala
                        <br /> <br />
					</span>
					

					<div class="wrap-input100 validate-input" data-validate = "El nombre de usuario es requerido">
						<input class="input100" type="text" name="username" runat="server" id="username"/>
						<span class="focus-input100" data-placeholder="Usuario"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate = "La contraseña es requerida">
						<span class="btn-show-pass">
							<i class="zmdi zmdi-eye"></i>
						</span>
						<input class="input100" type="password" name="pass" id="pass" runat="server"/>
						<span class="focus-input100" data-placeholder="Contraseña"></span>
					</div>

					<div class="container-login100-form-btn">
						<div class="wrap-login100-form-btn">
							<div class="login100-form-bgbtn"></div>
							 <button id="btn_ingreso" class="login100-form-btn" runat="server">Ingresar</button>
						</div>
					</div>
				</form>
			</div>
		</div>
	</div>
    	
<!--===============================================================================================-->
    <script src="Scripts/LoginCss/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
    <script src="Scripts/LoginCss/vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
    <script src="Scripts/LoginCss/vendor/bootstrap/js/popper.js"></script>
    <script src="Scripts/LoginCss/vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
    <script src="Scripts/LoginCss/vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
    <script src="Scripts/LoginCss/vendor/daterangepicker/moment.min.js"></script>
    <script src="Scripts/LoginCss/vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
    <script src="Scripts/LoginCss/vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
    <script src="Scripts/LoginCss/js/main.js"></script>

    <script src="Scripts/sweetalert2/sweetalert2.all.js"></script>
    <script src="Scripts/js/login.js"></script>
</body>
</html>
