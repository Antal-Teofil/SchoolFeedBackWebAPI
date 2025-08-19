import {create} from 'zustand'
import { persist, createJSONStorage } from 'zustand/middleware'
import {User} from "@/models/User"

type AuthStore ={
    user:User | null
    setUser: (u:User | null) => void
    clearUser : () => void
}

export const useAuthStore = create<AuthStore>()(
     persist(
    (set) => ({
      user: null,
      setUser: (u) => set({ user: u }),
      clearUser: () => set({ user: null }),
    }),
    {
      name: 'auth',
      storage: createJSONStorage(() => localStorage),
    }
  )
)