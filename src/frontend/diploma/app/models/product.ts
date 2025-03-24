export interface IProduct {
    id: number
    name: string
    price: number
    availableCount: number
    totalSold: number
    measurementType: MeasurementType
    isStock: boolean
    OrganizationId: number
}

export enum MeasurementType{
    Thing,
    Gram,
    Kg,
    Tones
}