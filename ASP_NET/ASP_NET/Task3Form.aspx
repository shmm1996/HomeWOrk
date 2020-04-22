<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Task3Form.aspx.cs" Inherits="ASP_NET.Task3Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<link rel="stylesheet" type="text/css" href="Style.css" />
</head>
<body>
	<main>
		<div class="border">
			<form runat="server" class="task3_grid">
				<div class="task3_grid_grid">
					<div class="t3_grid_3row">
						<h3>Storage</h3>
						<asp:ListBox ID="storageList" RepeatDirection="Vertical" SelectionMode="Multiple" runat="server" />
						<asp:Button runat="server" OnClick="OnClick_AddAll" Text="AddAll" />
					</div>
					<div class="t3_grid_2row">
						<h1></h1>
						<asp:Button runat="server" OnClick="OnClick_Add" Text=">>"/>
						<asp:Button runat="server" OnClick="OnClick_Remove" Text="<<"/>
					</div>
					<div class="t3_grid_3row">
						<h3>Cart</h3>
						<asp:ListBox ID="cartList" RepeatDirection="Vertical" SelectionMode="Multiple" runat="server" />
						<asp:Button runat="server" OnClick="OnClick_RemoveAll" Text="RemoveAll" />
					</div>
				</div>
			</form>
		</div>
	</main>
</body>
</html>
