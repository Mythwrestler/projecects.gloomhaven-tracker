{{/*
Expand the name of the chart.
*/}}
{{- define "helm.name" -}}
{{- default .Chart.Name .Values.ght.nameOverride | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "helm.fullname" -}}
{{- if .Values.ght.fullnameOverride }}
{{- .Values.ght.fullnameOverride | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- $name := default .Chart.Name .Values.ght.nameOverride }}
{{- if contains $name .Release.Name }}
{{- .Release.Name | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" }}
{{- end }}

{{- end }}
{{- end }}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "helm.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Commonlabels
*/}}
{{- define "helm.labels" -}}
helm.sh/chart: {{ include "helm.chart" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
Common Client labels
*/}}
{{- define "helm.ghtClient.labels" -}}
helm.sh/chart: {{ include "helm.chart" . }}
{{ include "helm.ghtClientSelectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
Common Service labels
*/}}
{{- define "helm.ghtService.labels" -}}
helm.sh/chart: {{ include "helm.chart" . }}
{{ include "helm.ghtServiceSelectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
GHT Client Selector labels
*/}}
{{- define "helm.ghtClientSelectorLabels" -}}
app.kubernetes.io/name: {{ include "helm.name" . }}-client
app.kubernetes.io/instance: {{ .Release.Name }}-client
{{- end }}

{{/*
GHT Service Selector labels
*/}}
{{- define "helm.ghtServiceSelectorLabels" -}}
app.kubernetes.io/name: {{ include "helm.name" . }}-service
app.kubernetes.io/instance: {{ .Release.Name }}-service
{{- end }}

{{/*
Create the name of the service account to use
*/}}
{{- define "helm.serviceAccountName" -}}
{{- if .Values.ght.serviceAccount.create }}
{{- default (include "helm.fullname" .) .Values.ght.serviceAccount.name }}
{{- else }}
{{- default "default" .Values.ght.serviceAccount.name }}
{{- end }}
{{- end }}
