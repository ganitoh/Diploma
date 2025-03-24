import { IOrder } from "./order"
import { IProduct } from "./product"

export interface IOrganiaiton {
    id: number
    name: string
    description: string
    inn: string
    email: string
    legalAddress: string
    products: IProduct []
    sellOrders: IOrder[]
    buyOrders: IOrder[]
}

export interface ICreateOrganiaiton{
    name: string
    description: string
    inn: string
    email: string
    legalAddress: string
}