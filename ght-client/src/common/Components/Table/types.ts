export interface TableConfiguration {
  columns: ColumnDefinition[];
}

export interface ColumnDefinition {
  position: number;
  property: string;
  header: HeaderDefinition;
  valueDisplayComponent?: unknown;
}

export interface HeaderDefinition {
  header?: unknown;
  headerProps?: unknown;
}

export interface RowData {
  [keys: string]: unknown;
}
