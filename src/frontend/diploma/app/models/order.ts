export interface IOrder {
    id: number
    totalPrice: number
    deliveryDate: Date
    createDate: Date
    status: OrderStatus
    sellerOrganizationId: number
    buyerOrganizationId: number
} 

enum OrderStatus
{
    Created = 1,
    Collected,
    InDelivery,
    Close
}