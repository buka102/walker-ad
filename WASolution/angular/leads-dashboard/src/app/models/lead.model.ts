export interface Lead {
    id: number;
    name: string;
    phoneNumber: string;
    zipCode: string;
    consentToContact: boolean;
    email?: string;
  }
  