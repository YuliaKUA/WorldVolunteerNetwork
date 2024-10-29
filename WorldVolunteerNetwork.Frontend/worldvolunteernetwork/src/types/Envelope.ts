type Envelope<T> = {
    result : T | null;
    errorInfo : ErrorInfo[] | null;
    timeGenerated: Date;
}

type ErrorInfo = {
    errorCode : string | null;
    errorMessage : string | null;
    invalidField : string | null;
}

export type {Envelope, ErrorInfo};