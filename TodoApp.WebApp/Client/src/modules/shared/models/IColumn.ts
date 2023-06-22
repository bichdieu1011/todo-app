import { FieldType } from "../enums/FieldType";

export interface IColumn{
    fieldName: string;
    displayName :string;
    sortable : boolean;
    type: FieldType;
}