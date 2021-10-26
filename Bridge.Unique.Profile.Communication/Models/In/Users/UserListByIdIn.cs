namespace Bridge.Unique.Profile.Communication.Models.In.Users
{
    /// <summary>
    ///     Objeto de transferência de dados de entrada de usuário.
    ///     Lista por array de identificadores
    /// </summary>
    public class UserListByIdIn
    {
        /// <summary>
        ///     Array de identificadores
        /// </summary>
        public int[] Ids { get; set; }
    }
}