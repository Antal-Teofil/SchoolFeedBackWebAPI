import React, { useState, useEffect } from 'react';
import { User, LogOut, Shield, GraduationCap } from 'lucide-react';

const GoogleAuthApp = () => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  // Replace with your actual Google OAuth2 Client ID
  const GOOGLE_CLIENT_ID = '637207134191-t75bngevukdts5fqq8mh0f30h7es0hn5.apps.googleusercontent.com';

  // Replace with your actual API base URL
  const API_BASE_URL = 'http://localhost:7277/api';

  useEffect(() => {
    // Load Google Sign-In API
    const script = document.createElement('script');
    script.src = 'https://accounts.google.com/gsi/client';
    script.async = true;
    script.defer = true;
    document.body.appendChild(script);

    script.onload = () => {
      const waitForGoogle = () => {
        if (window.google?.accounts?.id) {
          console.log("Google Identity fully loaded!");
          window.google.accounts.id.initialize({
            client_id: GOOGLE_CLIENT_ID,
            callback: handleGoogleResponse,
            auto_select: false,
          });
        } else {
          console.log("Waiting for Google Identity...");
          setTimeout(waitForGoogle, 100); // 100ms múlva újrapróbálkozunk
        }
      };
      waitForGoogle();
    };

    return () => {
      document.body.removeChild(script);
    };
  }, []);

  const handleGoogleResponse = async (response) => {
    setLoading(true);
    setError('');

    try {
      // Send the ID token to your backend
      const apiResponse = await fetch(`${API_BASE_URL}/auth/google`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        credentials: 'include', // Important for cookies
        body: JSON.stringify({
          IdToken: response.credential
        }),
      });

      if (!apiResponse.ok) {
        const errorText = await apiResponse.text();
        throw new Error(errorText || 'Authentication failed');
      }

      const userData = await apiResponse.json();
      setUser(userData);

    } catch (err) {
      setError(err.message);
      console.error('Authentication error:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleSignIn = () => {
    if (window.google) {
      window.google.accounts.id.prompt();
    }
  };
  /*
    const handleSignOut = async () => {
      try {
        // Optional: Call a logout endpoint to clear server-side session
        await fetch(`${API_BASE_URL}/auth/logout`, {
          method: 'POST',
          credentials: 'include',
        }).catch(() => {
          // Ignore errors for logout endpoint if it doesn't exist
        });
      } catch (err) {
        console.error('Logout error:', err);
      }
  
      // Clear local state
      setUser(null);
      setError('');
  
      // Clear Google Sign-In state
      if (window.google) {
        window.google.accounts.id.disableAutoSelect();
      }
    };
  */
  const UserProfile = ({ user }) => (
    <div className="bg-white rounded-lg shadow-lg p-8 max-w-md mx-auto">
      <div className="text-center mb-6">
        <div className="w-20 h-20 bg-blue-100 rounded-full flex items-center justify-center mx-auto mb-4">
          <User className="w-10 h-10 text-blue-600" />
        </div>
        <h2 className="text-2xl font-bold text-gray-900 mb-2">
          Welcome, {user.firstName}!
        </h2>
      </div>

      <div className="space-y-4">
        <div className="flex items-center p-3 bg-gray-50 rounded-lg">
          <User className="w-5 h-5 text-gray-500 mr-3" />
          <div>
            <p className="text-sm text-gray-500">Full Name</p>
            <p className="font-medium">{user.firstName} {user.lastName}</p>
          </div>
        </div>

        <div className="flex items-center p-3 bg-gray-50 rounded-lg">
          <span className="w-5 h-5 text-gray-500 mr-3">@</span>
          <div>
            <p className="text-sm text-gray-500">Email</p>
            <p className="font-medium">{user.email}</p>
          </div>
        </div>

        <div className="flex items-center p-3 bg-gray-50 rounded-lg">
          {user.role === 'Admin' ? (
            <Shield className="w-5 h-5 text-red-500 mr-3" />
          ) : (
            <GraduationCap className="w-5 h-5 text-blue-500 mr-3" />
          )}
          <div>
            <p className="text-sm text-gray-500">Role</p>
            <p className={`font-medium ${user.role === 'Admin' ? 'text-red-600' : 'text-blue-600'}`}>
              {user.role}
            </p>
          </div>
        </div>
      </div>


    </div>
  );

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 py-12 px-4">
      <div className="max-w-6xl mx-auto">
        <div className="text-center mb-12">
          <h1 className="text-4xl font-bold text-gray-900 mb-4">
            School Feedback System
          </h1>
          <p className="text-xl text-gray-600">
            Sign in with Google to access the platform
          </p>
        </div>

        {error && (
          <div className="max-w-md mx-auto mb-6 p-4 bg-red-50 border border-red-200 rounded-lg">
            <p className="text-red-800 text-center">{error}</p>
          </div>
        )}

        {loading && (
          <div className="text-center mb-6">
            <div className="inline-block animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
            <p className="mt-2 text-gray-600">Authenticating...</p>
          </div>
        )}

        {!user ? (
          <div className="text-center">
            <div className="bg-white rounded-lg shadow-lg p-8 max-w-md mx-auto">
              <div className="w-16 h-16 bg-blue-100 rounded-full flex items-center justify-center mx-auto mb-6">
                <Shield className="w-8 h-8 text-blue-600" />
              </div>

              <h3 className="text-xl font-semibold text-gray-900 mb-4">
                Sign In Required
              </h3>

              <p className="text-gray-600 mb-6">
                Please sign in with your Google account to access the school feedback system.
              </p>

              <button
                onClick={handleSignIn}
                disabled={loading}
                className="w-full bg-blue-600 hover:bg-blue-700 disabled:bg-blue-400 text-white font-medium py-3 px-6 rounded-lg transition-colors flex items-center justify-center"
              >
                <svg className="w-5 h-5 mr-3" viewBox="0 0 24 24">
                  <path fill="currentColor" d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z" />
                  <path fill="currentColor" d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z" />
                  <path fill="currentColor" d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z" />
                  <path fill="currentColor" d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z" />
                </svg>
                Sign in with Google
              </button>

              <div className="mt-6 text-xs text-gray-500">
                <p>Only authorized students and administrators can access this system.</p>
              </div>
            </div>
          </div>
        ) : (
          <UserProfile user={user} />
        )}

        <div className="mt-8 text-center text-sm text-gray-500">
          <p>Secure authentication powered by Google OAuth2</p>
        </div>
      </div>
    </div>
  );
};

export default GoogleAuthApp;