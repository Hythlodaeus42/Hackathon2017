

# --------------------------
# load data
path <- ''

busmat <- read.csv(paste(path, 'BusinessArchitectureMatrix.csv', sep = ''), stringsAsFactors=T)

# --------------------------
# add ordinals
busmat$BusinessFunctionGroupOrdinal <- as.numeric(busmat$BusinessFunctionGroup)
busmat$BusinessFunctionOrdinal <- as.numeric(busmat$BusinessFunction)
busmat$AssetClassOrdinal <- as.numeric(busmat$AssetClass)
busmat$ApplicationOrdinal<- as.numeric(busmat$Application)

# --------------------------
# write files
unique(busmat[, c("BusinessFunctionGroupOrdinal", "BusinessFunctionGroup")])

write.table(busmat, "BusinessArchitectureMatrix.csv", sep = ",", row.names = FALSE, quote=FALSE, col.names = FALSE)
write.table(unique(busmat[, c("BusinessFunctionGroupOrdinal", "BusinessFunctionGroup")]), "BusinessFunctionGroup.csv", sep = ",", row.names = FALSE, quote=FALSE, col.names = FALSE)
write.table(unique(busmat[, c("BusinessFunctionOrdinal", "BusinessFunction")]), "BusinessFunction.csv", sep = ",", row.names = FALSE, quote=FALSE, col.names = FALSE)
write.table(unique(busmat[, c("AssetClassOrdinal", "AssetClass")]), "AssetClass.csv", sep = ",", row.names = FALSE, quote=FALSE, col.names = FALSE)