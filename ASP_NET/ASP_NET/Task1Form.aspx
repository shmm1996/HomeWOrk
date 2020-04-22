<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Task1Form.aspx.cs" Inherits="ASP_NET.Task1Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Task 1</title>
	<link rel="stylesheet" type="text/css" href="Style.css" />
</head>
<body>
	<main>
		<div class="border">
			<div class="task1_grid">
				<h2>Выберете дни недели:</h2>
				<form runat="server">
					<asp:CheckBoxList ID="checkboxlist" RepeatDirection="Vertical" runat="server" />
				</form>
			</div>
		</div>
	</main>
</body>
</html>
