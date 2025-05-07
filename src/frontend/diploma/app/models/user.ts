export interface IUser {
    id: string
    email: string
    password: string
}

export interface ILoginUser{
    email: string
    password: string
}

export interface ICreateUser extends ILoginUser { }