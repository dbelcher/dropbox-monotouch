
	[BaseType (typeof (NSObject))]
	interface MPOAuthCredentialConcreteStore {
		[Export ("baseURL")]
		NSUrl BaseUrl { get;  }

		[Export ("authenticationURL")]
		NSUrl AuthenticationUrl { get;  }

		[Export ("tokenSecret")]
		string TokenSecret { get;  }

		[Export ("signingKey")]
		string SigningKey { get;  }

		[Export ("requestToken")]
		string RequestToken { get; set;  }

		[Export ("requestTokenSecret")]
		string RequestTokenSecret { get; set;  }

		[Export ("accessToken")]
		string AccessToken { get; set;  }

		[Export ("accessTokenSecret")]
		string AccessTokenSecret { get; set;  }

		[Export ("sessionHandle")]
		string SessionHandle { get; set;  }

		[Export ("initWithCredentials:")]
		IntPtr Constructor (NSDictionary inCredential);

		[Export ("initWithCredentials:forBaseURL:")]
		IntPtr Constructor (NSDictionary inCredentials, NSUrl inBaseURL);

		[Export ("initWithCredentials:forBaseURL:withAuthenticationURL:")]
		IntPtr Constructor (NSDictionary inCredentials, NSUrl inBaseURL, NSUrl inAuthenticationURL);

		[Export ("setCredential:withName:")]
		void SetCredentialwithName (NSObject inCredential, string inName);

		[Export ("removeCredentialNamed:")]
		void RemoveCredentialNamed (string inName);

	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface MPOAuthCredentialStore {
		[Abstract]
		[Export ("consumerKey")]
		string ConsumerKey { get;  }

		[Abstract]
		[Export ("consumerSecret")]
		string ConsumerSecret { get;  }

		[Abstract]
		[Export ("username")]
		string Username { get;  }

		[Abstract]
		[Export ("password")]
		string Password { get;  }

		[Abstract]
		[Export ("requestToken")]
		string RequestToken { get;  }

		[Abstract]
		[Export ("requestTokenSecret")]
		string RequestTokenSecret { get;  }

		[Abstract]
		[Export ("accessToken")]
		string AccessToken { get;  }

		[Abstract]
		[Export ("accessTokenSecret")]
		string AccessTokenSecret { get;  }

		[Abstract]
		[Export ("credentialNamed:")]
		string CredentialNamed (string inCredentialName);

		[Abstract]
		[Export ("discardOAuthCredentials")]
		void DiscardOAuthCredentials ();

	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface MPOAuthParameterFactory {
		[Abstract]
		[Export ("signatureMethod")]
		string SignatureMethod { get; set;  }

		[Abstract]
		[Export ("signingKey")]
		string SigningKey { get;  }

		[Abstract]
		[Export ("timestamp")]
		string Timestamp { get;  }

		[Abstract]
		[Export ("oauthParameters")]
		MPURLRequestParameter[] OAuthParameters ();

		[Abstract]
		[Export ("oauthConsumerKeyParameter")]
		MPURLRequestParameter OAuthConsumerKeyParameter ();

		[Abstract]
		[Export ("oauthTokenParameter")]
		MPURLRequestParameter OAuthTokenParameter ();

		[Abstract]
		[Export ("oauthSignatureMethodParameter")]
		MPURLRequestParameter OAuthSignatureMethodParameter ();

		[Abstract]
		[Export ("oauthTimestampParameter")]
		MPURLRequestParameter OAuthTimestampParameter ();

		[Abstract]
		[Export ("oauthNonceParameter")]
		MPURLRequestParameter OAuthNonceParameter ();

		[Abstract]
		[Export ("oauthVersionParameter")]
		MPURLRequestParameter OAuthVersionParameter ();

	}

	[BaseType (typeof (NSObject))]
	interface MPURLRequestParameter {
		[Export ("name")]
		string Name { get; set;  }

		[Export ("value")]
		string Value { get; set;  }

		[Static]
		[Export ("parametersFromString:")]
		NSArray ParametersFromString (string inString);

		[Static]
		[Export ("parametersFromDictionary:")]
		NSArray ParametersFromDictionary (NSDictionary inDictionary);

		[Static]
		[Export ("parameterDictionaryFromString:")]
		NSDictionary ParameterDictionaryFromString (string inString);

		[Static]
		[Export ("parameterStringForParameters:")]
		string ParameterStringForParameters (NSArray inParameters);

		[Static]
		[Export ("parameterStringForDictionary:")]
		string ParameterStringForDictionary (NSDictionary inParameterDictionary);

		[Export ("initWithName:andValue:")]
		NSObject InitWithNameandValue (string inName, string inValue);

		[Export ("URLEncodedParameterString")]
		string URLEncodedParameterString ();

	}
