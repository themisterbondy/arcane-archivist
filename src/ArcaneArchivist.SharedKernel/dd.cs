namespace ArcaneArchivist.SharedKernel;

public class Signature
{
    public string CanalEnvio { get; set; }
    public string CanalUtilizacao { get; set; }
    public Contrato Contrato { get; set; }
    public Certificados[] Certificados { get; set; }
    public Cliente Cliente { get; set; }
    public string DataAtualizacao { get; set; }
    public string DataCriacao { get; set; }
    public string Id { get; set; }
    public string IdEnvelope { get; set; }
    public string Status { get; set; }
    public Tags[] Tags { get; set; }
}

public class Contrato
{
    public string CodigoEmpresa { get; set; }
    public string CodigoFilial { get; set; }
    public string CodigoContrato { get; set; }
    public string CodigoVerificadorContrato { get; set; }
    public Comunicacao Comunicacao { get; set; }
    public Documentos[] Documentos { get; set; }
}

public class Comunicacao
{
    public string NumeroContratoFormatado { get; set; }
    public string NumeroParcelas { get; set; }
    public string ValorParcela { get; set; }
    public string ValorEntrada { get; set; }
    public string ValorTotalFinanciado { get; set; }
}

public class Documentos
{
    public string Nome { get; set; }
    public string Assinavel { get; set; }
    public string Tipo { get; set; }
}

public class Certificados
{
    public string Assinavel { get; set; }
    public string Bilhete { get; set; }
    public string Cobertura { get; set; }
    public string CodigoCertificado { get; set; }
    public string CodigoEmpresa { get; set; }
    public string CodigoFilial { get; set; }
    public string CodigoVerificadorCertificado { get; set; }
    public string Endereco { get; set; }
    public string FimVigencia { get; set; }
    public string InicioVigencia { get; set; }
    public string Produto { get; set; }
    public string TelefoneAtendimento { get; set; }
    public string TipoCertificado { get; set; }
}

public class Cliente
{
    public int Codigo { get; set; }
    public string Documento { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
    public Telefone Telefone { get; set; }
}

public class Telefone
{
    public string Ddd { get; set; }
    public string Numero { get; set; }
    public string Tipo { get; set; }
}

public class Tags
{
    public string Chave { get; set; }
    public string Valor { get; set; }
}