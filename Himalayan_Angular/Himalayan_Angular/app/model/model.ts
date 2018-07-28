export interface IBrand {
    BrandId: number;
    BrandName: string;
    LogoPath: string;
    PostedOn: Date;
    PostedBy: number;
}
export interface IClients {
    ClientId: number;
    ClientName: string;
    ClientAddress: string;
    Description: string;
    ImgPath: string;
    PostedOn: Date;
    PostedBy: number;
}
export interface IEnquiry {
    EnquiryId: number;
    ProductId: number;
    CustomerName: string;
    CustomerEmail: string;
    CustomerMobile: string;
    Message: string;
}
export interface IProductMain {
    id: number;
    BrandId: number;
    PTypeId: number;
    ProductName: string;
    Price: number;
    Size: string;
    ProductDetail: string;
    Color: string;
    Model: string;
    Thickness: string;
    Quantity: number;
    Path: string;
    PostedOn: Date;
    PostedBy: number;
}
export interface IProductType {
    PTypeId: number;
    PTypeName: string;
    PostedOn: Date;
    PostedBy: number;
}

export interface IUser {
    UserId: number;
    Name: string;
    UserName: string;
    PasswordHash: string;
    IsUnlimited: boolean;
    IsActive: boolean;
    SecurityStamp: string;
}