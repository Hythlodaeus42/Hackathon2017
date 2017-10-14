# install.packages("plotly")

library(igraph)

# -----------------------------------
# get files
path <- ''

nodes <- read.csv(paste(path, 'nodes.csv', sep = ''), stringsAsFactors=F)
edges <- read.csv(paste(path, 'edges.csv', sep = ''), stringsAsFactors=F)

# table(nodes$type)

# -----------------------------------
# constuct graph
g <- graph.data.frame(edges, directed = TRUE, vertices = nodes)

plot(g)


# -----------------------------------
# layout 2d
# -----------------------------------

l.fr <- layout_with_fr(g, dim = 2, niter = 500)
# l.kk <- layout_with_kk(g, dim = 2)

plot(g, layoyt = l.fr)
# plot(g, layoyt = l.kk)

# l.df <- data.frame(l.fr)
# names(l.df)
# names(nodes)
# l.df$node = nodes$node
# l.df$type = nodes$type


nodes.layout <- cbind(nodes, l.fr)

names(nodes.layout)[8:9] <- c("X", "Z")



# -----------------------------------
# centre the graph
nodes.layout$LayerOrdinal <- nodes.layout$LayerOrdinal - round(mean(nodes.layout$LayerOrdinal), 0)
nodes.layout$X <- nodes.layout$X - mean(nodes.layout$X)
nodes.layout$Z <- nodes.layout$Z - mean(nodes.layout$Z)

layer.x = ceiling(max(abs(nodes.layout$X)))
layer.z = ceiling(max(abs(nodes.layout$Z)))

# -----------------------------------
# set coords as vertex attribute 
V(g)$X <- nodes.layout$X
V(g)$Z <- nodes.layout$Z

# cbind(V(g)$name, V(g)$Y, V(g)$Z, nodes.layout$name, nodes.layout$Y, nodes.layout$Z)

# -----------------------------------
# get layers
layers <- unique(nodes[order(nodes$LayerOrdinal), c("LayerOrdinal", "Layer")])

layers$x = layer.x
layers$z = layer.z

# -----------------------------------
# write files
write_graph(g, "landscape.xml", format="graphml")
write.table(layers, "layers.csv", sep = ",", row.names = FALSE, quote=FALSE, col.names = FALSE)
