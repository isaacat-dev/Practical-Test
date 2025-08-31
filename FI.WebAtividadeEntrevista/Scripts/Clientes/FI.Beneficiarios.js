var beneficiarios = [];
var beneficiarioEditando = null;

$(document).ready(function () {
    $('#cpfBeneficiario').on('input', function () {
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

        var add = 0;
        for (var i = 0; i < 9; i++)
            add += parseInt(cpf.charAt(i)) * (10 - i);
        var rev = 11 - (add % 11);
        if (rev == 10 || rev == 11)
            rev = 0;
        if (rev != parseInt(cpf.charAt(9)))
            return false;

        add = 0;
        for (var i = 0; i < 10; i++)
            add += parseInt(cpf.charAt(i)) * (11 - i);
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11)
            rev = 0;
        if (rev != parseInt(cpf.charAt(10)))
            return false;
        return true;
    }

    $('#btnBeneficiarios').click(function () {
        $('#modalBeneficiarios').modal('show');
        carregarBeneficiarios();
    });

    $('#btnIncluirBeneficiario').click(function () {
        var cpf = $('#cpfBeneficiario').val();
        var nome = $('#nomeBeneficiario').val();

        if (!cpf || !nome) {
            ModalDialog("Erro de validação", "Por favor, preencha todos os campos");
            return;
        }

        if (!validarCPF(cpf)) {
            ModalDialog("Erro de validação", "CPF inválido");
            return;
        }

        if (beneficiarios.some(b => b.cpf === cpf)) {
            ModalDialog("Erro de validação", "CPF já cadastrado para este cliente");
            return;
        }

        var beneficiario = {
            id: 0,
            cpf: cpf,
            nome: nome
        };

        beneficiarios.push(beneficiario);
        atualizarTabelaBeneficiarios();
        limparCamposBeneficiario();
    });

    $('#btnAlterarBeneficiario').click(function () {
        var cpf = $('#cpfBeneficiario').val();
        var nome = $('#nomeBeneficiario').val();

        if (!cpf || !nome) {
            ModalDialog("Erro de validação", "Por favor, preencha todos os campos");
            return;
        }

        if (!validarCPF(cpf)) {
            ModalDialog("Erro de validação", "CPF inválido");
            return;
        }

        if (beneficiarios.some(b => b.cpf === cpf && b !== beneficiarioEditando)) {
            ModalDialog("Erro de validação", "CPF já cadastrado para este cliente");
            return;
        }

        beneficiarioEditando.cpf = cpf;
        beneficiarioEditando.nome = nome;

        atualizarTabelaBeneficiarios();
        cancelarEdicaoBeneficiario();
    });

    $('#btnCancelarBeneficiario').click(function () {
        cancelarEdicaoBeneficiario();
    });

    function carregarBeneficiarios() {
        if (obj && obj.Beneficiarios) {
            beneficiarios = obj.Beneficiarios.map(b => ({
                id: b.Id,
                cpf: b.CPF,
                nome: b.Nome
            }));
        }
        atualizarTabelaBeneficiarios();
    }

    // Função para atualizar tabela
    window.atualizarTabelaBeneficiarios = function() {
        var tbody = $('#corpoTabelaBeneficiarios');
        tbody.empty();

        beneficiarios.forEach(function (beneficiario, index) {
            var row = '<tr>' +
                '<td>' + beneficiario.cpf + '</td>' +
                '<td>' + beneficiario.nome + '</td>' +
                '<td>' +
                '<button type="button" class="btn btn-sm btn-primary" onclick="editarBeneficiario(' + index + ')">Alterar</button> ' +
                '<button type="button" class="btn btn-sm btn-primary" onclick="excluirBeneficiario(' + index + ')">Excluir</button>' +
                '</td>' +
                '</tr>';
            tbody.append(row);
        });
    }

    // Função para limpar campos
    function limparCamposBeneficiario() {
        $('#cpfBeneficiario').val('');
        $('#nomeBeneficiario').val('');
    }

    // Função para cancelar edição
    function cancelarEdicaoBeneficiario() {
        beneficiarioEditando = null;
        limparCamposBeneficiario();
        $('#btnIncluirBeneficiario').show();
        $('#btnAlterarBeneficiario').hide();
        $('#btnCancelarBeneficiario').hide();
    }

    // Função global para editar beneficiário
    window.editarBeneficiario = function (index) {
        beneficiarioEditando = beneficiarios[index];
        $('#cpfBeneficiario').val(beneficiarioEditando.cpf);
        $('#nomeBeneficiario').val(beneficiarioEditando.nome);
        $('#btnIncluirBeneficiario').hide();
        $('#btnAlterarBeneficiario').show();
        $('#btnCancelarBeneficiario').show();
    };

    // Função global para excluir beneficiário
    window.excluirBeneficiario = function (index) {
        if (confirm('Deseja realmente excluir este beneficiário?')) {
            beneficiarios.splice(index, 1);
            atualizarTabelaBeneficiarios();
        }
    };

});