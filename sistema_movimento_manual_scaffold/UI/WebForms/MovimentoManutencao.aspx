<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovimentoManutencao.aspx.cs" Inherits="UI.WebForms.MovimentoManutencao" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Manutenção Movimento Manual</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Mês: <asp:TextBox ID="txtMes" runat="server" />
        Ano: <asp:TextBox ID="txtAno" runat="server" /><br />
        Produto: <asp:DropDownList ID="ddlProduto" runat="server" /><br />
        Cosif: <asp:DropDownList ID="ddlCosif" runat="server" /><br />
        Valor: <asp:TextBox ID="txtValor" runat="server" /><br />
        Descrição: <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" Rows="4" Columns="60" /><br />
        <asp:Button ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
        <asp:Button ID="btnNovo" runat="server" Text="Novo" OnClick="btnNovo_Click" />
        <asp:Button ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" /><br />
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="true"></asp:GridView>
    </div>
    </form>
</body>
</html>
