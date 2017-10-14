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

names(nodes.layout)[8:9] <- c("Z", "Y")



# -----------------------------------
# centre the graph
nodes.layout$LayerOrdinal <- nodes.layout$LayerOrdinal - round(mean(nodes.layout$LayerOrdinal), 0)
nodes.layout$Y <- nodes.layout$Y - mean(nodes.layout$Y)
nodes.layout$Z <- nodes.layout$Z - mean(nodes.layout$Z)

# -----------------------------------
# set coords as vertex attribute 
V(g)$Y <- nodes.layout$Y
V(g)$Z <- nodes.layout$Z

# cbind(V(g)$name, V(g)$Y, V(g)$Z, nodes.layout$name, nodes.layout$Y, nodes.layout$Z)

# -----------------------------------
# write csv files
# write.csv(nodes.layout, "nodes_layout.csv", row.names = FALSE, quote=FALSE, col.names = FALSE)

write_graph(g, "landscape.xml", format="graphml")

