import { useState } from 'react'
import { GoogleLogin } from '@react-oauth/google'

const API_BASE_URL = 'http://localhost:7277/api'

export default function GoogleAuthApp() {
  const [user, setUser] = useState<any>(null)
  const [error, setError] = useState('')

  const onIdTokenSuccess = async (resp: any) => {
    try {
      const idToken = resp?.credential
      console.log(idToken);
      const r = await fetch(`${API_BASE_URL}/auth/google`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        body: JSON.stringify({ IdToken: idToken })
      })
      if (!r.ok) throw new Error(await r.text() || 'Auth failed')
      setUser(await r.json())
    } catch (e: any) { setError(e.message) }
  }

  return (
    <div>
      {!user ? (
        <>
          <GoogleLogin
            onSuccess={onIdTokenSuccess}
            onError={() => setError('Login failed')}
            useOneTap
            auto_select
          />
          {error && <p style={{ color: 'red' }}>{error}</p>}
        </>
      ) : (
        <div>Welcome, {user.firstName}</div>
      )}
    </div>
  )
}
