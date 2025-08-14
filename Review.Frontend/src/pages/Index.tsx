import { GoogleLogin } from '@react-oauth/google'
import { useNavigate } from 'react-router-dom'
import { useReviews } from '@/hooks/useReviews' // innen jön a loginWithGoogle mutáció
import { useQueryClient } from '@tanstack/react-query'

export default function GoogleAuthApp() {
  const navigate = useNavigate()
  const client = useQueryClient()

  const {  loginWithGoogle,isLoggingIn } = useReviews()

  const onIdTokenSuccess = (resp: any) => {
    const idToken = resp?.credential
    if (!idToken) {
      console.error("No ID token from Google")
      return
    }

    // Meghívjuk a mutációt
    loginWithGoogle(idToken, {
      onSuccess: (user) => {
        // Az adat már itt van, beállítjuk cache-be
        client.setQueryData(['user'], user)

        // Role alapú navigáció
        if (user.role === 'Admin') {
          navigate("/dashboard/admin")
        } else if (user.role === 'Student') {
          navigate("/dashboard/student/")
        } else {
          navigate("/no-access")
        }
      },
      onError: (e: any) => {
        if (e?.response?.status === 403) {
          navigate("/no-access")
        } else {
          console.error(e)
        }
      }
    })
  }

  return (
    <div>
      <GoogleLogin
        onSuccess={onIdTokenSuccess}
        onError={() => console.error("Login failed")}
        useOneTap
        auto_select
      />
      {isLoggingIn && <p>Logging in...</p>}
      {loginError && <p style={{ color: 'red' }}>{(loginError as any)?.message}</p>}
    </div>
  )
}
