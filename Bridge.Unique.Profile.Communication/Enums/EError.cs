namespace Bridge.Unique.Profile.Communication.Enums
{
    /// <summary>
    ///     Enumerador de Erros
    /// </summary>
    public enum EError
    {
        //User
        /// <summary>
        ///     Usuário não encontrado
        /// </summary>
        USER_NOT_FOUND = 1000,

        /// <summary>
        ///     Usuário já existente
        /// </summary>
        USER_ALREADY_EXISTS = 1001,

        /// <summary>
        ///     Usuário já existe e foi associado com a nova aplicação
        /// </summary>
        USER_EXISTS_UPDATED_WITH_NEW_APPLICATION = 1002,

        /// <summary>
        ///     Token de acesso expirado
        /// </summary>
        ACCESS_TOKEN_EXPIRED = 1003,

        /// <summary>
        ///     Token atualizado expirado
        /// </summary>
        REFRESH_TOKEN_EXPIRED = 1004,

        /// <summary>
        ///     Parâmetros de login incorretos
        /// </summary>
        WRONG_LOGIN_PARAMETERS = 1005,

        /// <summary>
        ///     Tamanho mínimo de senha
        /// </summary>
        PASSWORD_MINIMUM_LENGTH = 1006,

        /// <summary>
        ///     Tamonho mínimo de token
        /// </summary>
        REFRESH_TOKEN_MINIMUM_LENGTH = 1007,

        /// <summary>
        ///     Usuário ou senha incorretos
        /// </summary>
        USER_OR_PASSWORD_INCORRECT = 1008,

        /// <summary>
        ///     Login social de usuário invalido
        /// </summary>
        USER_SOCIAL_INVALID = 1009,

        /// <summary>
        ///     Senha incorreta
        /// </summary>
        WRONG_PASSWORD = 1010,

        /// <summary>
        ///     O campo 'Name' deve contar o nome completo do usuário
        /// </summary>
        NAME_NOT_COMPLETE = 1011,

        /// <summary>
        ///     Usuário já existe e foi associado com a nova aplicação, mas precisa de aprovação de cadastro
        /// </summary>
        USER_EXISTS_UPDATED_WITH_NEW_APPLICATION_BUT_NEEDS_APPROVAL = 1012,

        /// <summary>
        ///     Usuário já está vinculado em outro portal.
        /// </summary>
        USER_ALREADY_EXISTS_IN_PORTAL = 1013,

        //Phone +50
        /// <summary>
        ///     Código de validação de telefone incorreto
        /// </summary>
        WRONG_PHONE_VALIDATION_CODE = 1050,

        /// <summary>
        ///     Telefone não validado
        /// </summary>
        PHONE_NOT_VALIDATED = 1051,

        /// <summary>
        ///     Conta social sem telefone
        /// </summary>
        SOCIAL_ACCOUNT_WITHOUT_PHONE = 1052,

        /// <summary>
        ///     Telefone já validado
        /// </summary>
        PHONE_ALREADY_VALIDATED = 1053,

        /// <summary>
        ///     Tamanho do código de telefone incorreto
        /// </summary>
        WRONG_PHONE_CODE_LENGTH = 1054,

        /// <summary>
        ///     Tamanho do número de telefone incorreto
        /// </summary>
        WRONG_PHONE_NUMBER_LENGTH = 1055,

        //Email +50
        /// <summary>
        ///     Token de validação de e-mail incorreto
        /// </summary>
        WRONG_EMAIL_VALIDATION_TOKEN = 1100,

        /// <summary>
        ///     E-mail não validado
        /// </summary>
        EMAIL_NOT_VALIDATED = 1101,

        /// <summary>
        ///     Conta social sem e-mail
        /// </summary>
        SOCIAL_ACCOUNT_WITHOUT_EMAIL = 1102,

        /// <summary>
        ///     E-mail já validado
        /// </summary>
        EMAIL_ALREADY_VALIDATED = 1103,

        /// <summary>
        ///     Formato de e-mail invalido
        /// </summary>
        WRONG_EMAIL_FORMAT = 1104,

        /// <summary>
        ///     O acesso ainda não foi aprovado
        /// </summary>
        ACCESS_NOT_APPROVED = 1105,

        /// <summary>
        ///     O acesso já foi aprovado
        /// </summary>
        ACCESS_ALREADY_APPROVED = 1106,

        //Address + 50
        /// <summary>
        ///     Endereço não encontrado
        /// </summary>
        ADDRESS_NOT_FOUND = 1150,

        /// <summary>
        ///     Cep não encontrado
        /// </summary>
        ADDRESS_ZIPCODE_NOT_FOUND = 1151,

        //General +100
        /// <summary>
        ///     API não autorizada
        /// </summary>
        API_UNAUTHORIZED = 1200,

        /// <summary>
        ///     Cliente não autorizado
        /// </summary>
        CLIENT_UNAUTHORIZED = 1201,

        /// <summary>
        ///     Login social não autorizado
        /// </summary>
        SOCIAL_UNAUTHORIZED = 1202,

        /// <summary>
        ///     Cliente já cadastrado
        /// </summary>
        CLIENT_ALREADY_EXISTS = 1203,

        /// <summary>
        ///     Cliente não pode ser cadastrado
        /// </summary>
        CLIENT_COULD_NOT_BE_REGISTERED = 1204
    }
}