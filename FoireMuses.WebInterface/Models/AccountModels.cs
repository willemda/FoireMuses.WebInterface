using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FoireMuses.WebInterface.Models
{

	#region Services
	// Le type de FormsAuthentication est sealed et contient des membres statiques ; par conséquent, il est difficile
	// d'effectuer un test unitaire sur du code qui appelle ses membres. L'interface et la classe d'assistance ci-dessous montrent
	// comment créer un wrapper abstrait autour d'un tel type afin de pouvoir tester
	// l'unité de code AccountController.

	public interface IMembershipService
	{
		bool ValidateUser(string userName, string password);
		MembershipCreateStatus CreateUser(string userName, string password, string email);
	}

	public class AccountMembershipService : IMembershipService
	{
		private readonly MembershipProvider _provider;

		public AccountMembershipService()
			: this(null)
		{
		}

		public AccountMembershipService(MembershipProvider provider)
		{
			_provider = provider ?? Membership.Provider;
		}

		public bool ValidateUser(string userName, string password)
		{
			if (String.IsNullOrEmpty(userName)) throw new ArgumentException("La valeur ne peut pas être null ou vide.", "userName");
			if (String.IsNullOrEmpty(password)) throw new ArgumentException("La valeur ne peut pas être null ou vide.", "password");

			return _provider.ValidateUser(userName, password);
		}

		public MembershipCreateStatus CreateUser(string userName, string password, string email)
		{
			if (String.IsNullOrEmpty(userName)) throw new ArgumentException("La valeur ne peut pas être null ou vide.", "userName");
			if (String.IsNullOrEmpty(password)) throw new ArgumentException("La valeur ne peut pas être null ou vide.", "password");
			if (String.IsNullOrEmpty(email)) throw new ArgumentException("La valeur ne peut pas être null ou vide.", "email");

			MembershipCreateStatus status;
			_provider.CreateUser(userName, password, email, null, null, true, null, out status);
			return status;
		}
	}

	public interface IFormsAuthenticationService
	{
		void SignIn(string userName, bool createPersistentCookie);
		void SignOut();
	}

	public class FormsAuthenticationService : IFormsAuthenticationService
	{
		public void SignIn(string userName, bool createPersistentCookie)
		{
			if (String.IsNullOrEmpty(userName)) throw new ArgumentException("La valeur ne peut pas être null ou vide.", "userName");

			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
		}

		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}
	}
	#endregion

	#region Validation
	public static class AccountValidation
	{
		public static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// Consultez http://go.microsoft.com/fwlink/?LinkID=177550 pour
			// obtenir la liste complète des codes d'état.
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "Le nom d'utilisateur existe déjà. Entrez un nom d'utilisateur différent.";

				case MembershipCreateStatus.DuplicateEmail:
					return "Un nom d'utilisateur pour cette adresse de messagerie existe déjà. Entrez une adresse de messagerie différente.";

				case MembershipCreateStatus.InvalidPassword:
					return "Le mot de passe fourni n'est pas valide. Entrez une valeur de mot de passe valide.";

				case MembershipCreateStatus.InvalidEmail:
					return "L'adresse de messagerie fournie n'est pas valide. Vérifiez la valeur et réessayez.";

				case MembershipCreateStatus.InvalidAnswer:
					return "La réponse de récupération du mot de passe fournie n'est pas valide. Vérifiez la valeur et réessayez.";

				case MembershipCreateStatus.InvalidQuestion:
					return "La question de récupération du mot de passe fournie n'est pas valide. Vérifiez la valeur et réessayez.";

				case MembershipCreateStatus.InvalidUserName:
					return "Le nom d'utilisateur fourni n'est pas valide. Vérifiez la valeur et réessayez.";

				case MembershipCreateStatus.ProviderError:
					return "Le fournisseur d'authentification a retourné une erreur. Vérifiez votre entrée et réessayez. Si le problème persiste, contactez votre administrateur système.";

				case MembershipCreateStatus.UserRejected:
					return "La demande de création d'utilisateur a été annulée. Vérifiez votre entrée et réessayez. Si le problème persiste, contactez votre administrateur système.";

				default:
					return "Une erreur inconnue s'est produite. Vérifiez votre entrée et réessayez. Si le problème persiste, contactez votre administrateur système.";
			}
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class PropertiesMustMatchAttribute : ValidationAttribute
	{
		private const string _defaultErrorMessage = "'{0}' et '{1}' ne correspondent pas.";
		private readonly object _typeId = new object();

		public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
			: base(_defaultErrorMessage)
		{
			OriginalProperty = originalProperty;
			ConfirmProperty = confirmProperty;
		}

		public string ConfirmProperty { get; private set; }
		public string OriginalProperty { get; private set; }

		public override object TypeId
		{
			get
			{
				return _typeId;
			}
		}

		public override string FormatErrorMessage(string name)
		{
			return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
				OriginalProperty, ConfirmProperty);
		}

		public override bool IsValid(object value)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
			object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
			object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
			return Object.Equals(originalValue, confirmValue);
		}
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
	{
		private const string _defaultErrorMessage = "'{0}' doit comporter au moins {1} caractères.";
		private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

		public ValidatePasswordLengthAttribute()
			: base(_defaultErrorMessage)
		{
		}

		public override string FormatErrorMessage(string name)
		{
			return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
				name, _minCharacters);
		}

		public override bool IsValid(object value)
		{
			string valueAsString = value as string;
			return (valueAsString != null && valueAsString.Length >= _minCharacters);
		}
	}
	#endregion

}
