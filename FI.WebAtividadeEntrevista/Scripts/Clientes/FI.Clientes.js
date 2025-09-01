$(document).ready(function () {
    $('#CPF').on('input', function () {
        var value = this.value.replace(/\D/g, '');
        value = value.replace(/(\d{3})(\d)/, '$1.$2');
        value = value.replace(/(\d{3})(\d)/, '$1.$2');
        value = value.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
        this.value = value;
    });

    function validarCPF(cpf) {
        cpf = cpf.replace(/[^\d]+/g, '');
        if (cpf.length != 11 ||
            cpf == "00000000000" ||
            cpf == "11111111111" ||
            cpf == "22222222222" ||
            cpf == "33333333333" ||
            cpf == "44444444444" ||
            cpf == "55555555555" ||
            cpf == "66666666666" ||
            cpf == "77777777777" ||
            cpf == "88888888888" ||
            cpf == "99999999999")
            return false;

        add = 0;
        for (i = 0; i < 9; i++)
            add += parseInt(cpf.charAt(i)) * (10 - i);
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11)
            rev = 0;
        if (rev != parseInt(cpf.charAt(9)))
            return false;

        add = 0;
        for (i = 0; i < 10; i++)
            add += parseInt(cpf.charAt(i)) * (11 - i);
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11)
            rev = 0;
        if (rev != parseInt(cpf.charAt(10)))
            return false;
        return true;
    }

    if (!$('#formCadastro').data('submit-handler-attached')) {
        $('#formCadastro').data('submit-handler-attached', true);

        $('#formCadastro').off('submit').on('submit', function (e) {
            e.preventDefault();

            if ($(this).data('submitting')) {
                return false;
            }
            $(this).data('submitting', true);

            var cpf = $('#CPF').val();
            if (!validarCPF(cpf)) {
                $(this).data('submitting', false);
                ModalDialog("Erro de validação", "CPF inválido");
                return false;
            }

            var beneficiariosData = typeof beneficiarios !== 'undefined' ? beneficiarios.map(b => ({
                Id: b.id || 0,
                CPF: b.cpf,
                Nome: b.nome,
                IdCliente: 0
            })) : [];

            var $form = $(this);

            $.ajax({
                url: urlPost,
                method: "POST",
                data: {
                    "NOME": $form.find("#Nome").val(),
                    "CEP": $form.find("#CEP").val(),
                    "CPF": $form.find("#CPF").val(),
                    "Email": $form.find("#Email").val(),
                    "Sobrenome": $form.find("#Sobrenome").val(),
                    "Nacionalidade": $form.find("#Nacionalidade").val(),
                    "Estado": $form.find("#Estado").val(),
                    "Cidade": $form.find("#Cidade").val(),
                    "Logradouro": $form.find("#Logradouro").val(),
                    "Telefone": $form.find("#Telefone").val(),
                    "Beneficiarios": beneficiariosData
                },
                error: function (r) {
                    $form.data('submitting', false);
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
                success: function (r) {
                    $form.data('submitting', false);
                    ModalDialog("Sucesso!", r);
                    $("#formCadastro")[0].reset();
                    if (typeof beneficiarios !== 'undefined') {
                        beneficiarios = [];
                        if (typeof atualizarTabelaBeneficiarios === 'function') {
                            atualizarTabelaBeneficiarios();
                        }
                    }
                }
            });
        });
    }
});

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
