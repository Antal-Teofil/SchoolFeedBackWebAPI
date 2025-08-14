import {create} from 'zustand'

type User={
    firstName: string
    lastName : string
    email : string
    role : string
} | null

type AuthStore ={
    user:User
    setUser: (u:User) => void
    clearUser : () => void
}

export const useAuthStore = create<AuthStore>((set) => ({
    user: null,
    setUser : (u) => set({user : u}),
    clearUser : () => set({user : null})
}))